using System;
using System.Collections.Generic;
using UnityEngine;

namespace script
{
    public class Person : ICloneable {

        public PersonController personController;

        public int id;
        public string name;
        public int level = 1;
        public int experience;
        public int powerCost;
        public int powerCostPerLevel;

        public float health;
        public float shield;
        public float maxHealth;
        public float basicHealth;

        public float mana;
        public float maxMana;
        public float basicMana;

        public int numberParrallelCasts = 1;

        public string skillSet;
        public string personImage = "";
        public string personModel = "";

        public string resource;

        public AbilityTargetType enemy;
        public AbilityTargetType ally;

        public List<Ability> abilityList = new List<Ability>();
        public List<Buff> effectList = new List<Buff>();

        public List<Ability> knownAbilities = new List<Ability>();
        public ActiveInventory activeInventory = new ActiveInventory();

        public List<Ability> usedAbilites = new List<Ability>();

        public Person summoner = null;
        public Vector2 place;

        public int agro;

        public bool isAlive;
        public bool updateBuffs = true;
        public bool isActive = false;

        public readonly PersonStatistics statistics = new PersonStatistics();

        public bool hasEffect(Buff b) {
            return effectList.FindAll(buff => buff.name.Equals(b.name)).Count > 0;
        }

        public void addEffect(Buff b) {
            effectList.Add(b);
            updateBuffs = true;
        }

        public void removeEffect(Buff b) {
            effectList.Remove(b);
            updateBuffs = true;
        }

        public bool isDamaged() {
            return maxHealth > health;
        }

        public float damage(Ability ability) {
            float resultValue = 0.0f;
            if (isAlive) {
                foreach (var t in ability.effectList) {
                    resultValue += t.value;
                }
                var afterShield = resultValue;
                if (shield > 0) {
                    if (shield >= resultValue) {
                        shield -= resultValue;
                        afterShield = 0;
                        ability.effectList.ForEach(ae 
                            => ae.attribures.Add(EffectAttribures.MAGIC_SHIELD));
                    }else {
                        afterShield -= shield;
                        shield = 0;
                    }
                }

                health -= afterShield;

                if (health < 0) {
                    health = 0;
                    isAlive = false;
                }
            }

            if (ability.animation != null) {
                personController.applyEffect();
            }

            return resultValue;
        }

        public float heal(Ability ability) {
            float resultValue = health;
            if (isAlive) {
                for (int i = 0; i < ability.effectList.Count; i++) {
                    health += ability.effectList[i].value;
                }
                if (health > maxHealth) {
                    health = maxHealth;
                }
            }
            return health - resultValue;
        }

        public float addShield(Ability ability) {
            float resultValue = shield;
            if (isAlive) {
                for (int i = 0; i < ability.effectList.Count; i++) {
                    shield += ability.effectList[i].value;
                }
            }
            return shield - resultValue;
        }

        public virtual float eventStart(Ability ability, float eventStartTime) {
            float time = 0.0f;
            if (!isAlive) return time;
            if (ability.GetType() != typeof (ActiveBuff)) {
                generateNextActiveEvent(eventStartTime);
                generateCooldownEvent(ability, eventStartTime);
            }
            time = ability.eventStart(eventStartTime);
            return time;
        }

        private void generateNextActiveEvent(float eventStartTime) {

            var e = new GenerateAbilityEvent {
                eventTime = eventStartTime,
                owner = this
            };
            EventQueueSingleton.queue.add(e);
        }

        protected void generateCooldownEvent(Ability ability, float eventStartTime) {
            var e = new CooldownEvent {
                eventTime = eventStartTime + ability.cooldown,
                ability = ability,
                owner = this
            };
            EventQueueSingleton.queue.add(e);
        }

        public void generateEvents(float eventTime) {
            for (int i = 0; i < numberParrallelCasts; i++) {
                generateNextActiveEvent(eventTime);
            }
            foreach (var buff in effectList) {
                buff.generateEvents(this, eventTime);
            }
        }

        public Person()
        {
            maxHealth = Constants.PERSON_BASE_HEALTH;
            maxMana = Constants.PERSON_BASE_MANA;
            isAlive = true;

            agro = Constants.PERSON_AGRO;

            knownAbilities.Add(new MeleeAttack("Melee Attack"));
        }

        public object Clone() {
            var newPerson = (Person) MemberwiseClone();
            foreach (var ab in newPerson.knownAbilities) {
                ab.setPerson(newPerson);
            }
            foreach (var item in newPerson.activeInventory.getItemList()) {
                item.owner = newPerson;
                foreach(var ability in item.abilityList) {
                    ability.abilityTactic.person = newPerson;
                    ability.personOwner = newPerson;
                }
            }
            return newPerson;
        }

        public void initHealthMana() {
            maxMana = basicMana * Constants.getMultiplayer(level);
            maxHealth = basicHealth * Constants.getMultiplayer(level);

            mana = maxMana;
            health = maxHealth;
        }

        public virtual void initAbilities() {
            abilityList.Clear();
            effectList.Clear();
            usedAbilites.Clear();

            foreach (var ability in knownAbilities) {
                if (ability.isActive
                    && ability.requiredLevel <= level) {
                    if (ability.type.Equals("passiveAbility")
                        || ability.type.Equals("activeBuff")) {
                        effectList.Add((Buff)ability);
                    } else {
                        abilityList.Add(ability);
                    }
                }
            }

            for (int i = 0; i < activeInventory.getItemList().Count; i++) {
                if (activeInventory.getItemList()[i].modificatorList.Count > 0) {
                    var itemBuff = new Buff();
                    itemBuff.setAbstractTactic(new MeleeAttackTactic());
                    itemBuff.name = activeInventory.getItemList()[i].name;
                    itemBuff.modificator = activeInventory.getItemList()[i].modificatorList[0];
                    itemBuff.image = activeInventory.getItemList()[i].image;
                    effectList.Add(itemBuff);
                }
                abilityList.AddRange(activeInventory.getItemList()[i].abilityList);
                foreach (var a in activeInventory.getItemList()[i].userAbilityList) {
                    a.setPerson(PartiesSingleton.player);
                    a.animationTime = 0.0f;
                    a.playerCastCount = activeInventory.getItemList()[i].maxDurability;
                    PartiesSingleton.player.abilityList.Add(a);
                }
            }

            foreach (var ab in abilityList) {
                ab.level = level;
                ab.initAbility();
            }
            foreach (var ab in effectList) {
                ab.level = level;
                ab.initAbility();
            }
            statistics.reset();
        }

        public void updateAgro(int value) {
            if (value > agro && value <= agro * 3) {
                agro = value;
            }
        }

        public void unSummon() {
            health = 0;
        }

        public int calculatePower() {
            int total = powerCost + powerCostPerLevel * (level - 1);
            foreach (var item in activeInventory.getItemList()) {
                total += item.powerCost + item.powerCostPerLevel * (level - 1);
            }
            return total;
        }

        public void setLevel(int newLevel) {
            level = newLevel;
            foreach (var item in activeInventory.getItemList()) {
                item.setLevel(level);
            }
            foreach (var ability in knownAbilities) {
                ability.setLevel(level);
            }
        }

        public Item findItem(ItemType type) {
            foreach (var item in activeInventory.getItemList()) {
                if (item.type == type) {
                    return item;
                }
            }
            return null;
        }

        public Ability findAbility(string abilityResource) {
            return knownAbilities.Find(ability => abilityResource.Equals(ability.resource));
        }

        public void setExpirience(int exp) {
            experience = exp;

            int currentLevel = 100;
            int prevLevel = 0;

            for (int i = 1; i < level; i++) {
                int tempLevel = currentLevel;
                currentLevel += prevLevel;
                prevLevel = tempLevel;
            }

            while (experience >= currentLevel) {
                experience -= currentLevel;
                level++;

                int tempLevel = currentLevel;
                currentLevel += prevLevel;
                prevLevel = tempLevel;
            }
        }
    }
}
