using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    public String skillSet;
    public String personImage = "";
    public String personModel = "";

    public AbilityTargetType enemy;
    public AbilityTargetType ally;

    public List<Ability> abilityList = new List<Ability>();
    public List<Buff> effectList = new List<Buff>();

    public List<Ability> knownAbilities = new List<Ability>();
    public List<Item> itemList = new List<Item>();

    public List<Ability> usedAbilites = new List<Ability>();

    public Person summoner = null;
    public Vector2 place;

    public int agro;

    public bool isAlive;
    public bool updateBuffs = true;
    public bool isActive = false;

    public PersonStatistics statistics = new PersonStatistics();

    public bool hasEffect(Buff b) {
        if (effectList.FindAll((Buff buff) => buff.name.Equals(b.name)).Count > 0) {
            return true;
        }
        return false;
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
        float afterShield = 0.0f;
        if (isAlive) {
            for (int i = 0; i < ability.effectList.Count; i++) {
                resultValue += ability.effectList[i].value;
            }
            afterShield = resultValue;
            if (shield > 0) {
                if (shield >= resultValue) {
                    shield -= resultValue;
                    afterShield = 0;
                    ability.effectList.ForEach((AbstractAbilityEffect ae) 
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
        if (isAlive) {
            if (ability.GetType() != typeof (ActiveBuff)) {
                generateNextActiveEvent(eventStartTime);
                generateCooldownEvent(ability, eventStartTime);
            }
            time = ability.eventStart(eventStartTime);
        }
        return time;
    }

    public void generateNextActiveEvent(float eventStartTime) {

        GenerateAbilityEvent e = new GenerateAbilityEvent();
        e.eventTime = eventStartTime;
        e.owner = this;
        EventQueueSingleton.queue.add(e);
    }

    public void generateCooldownEvent(Ability ability, float eventStartTime) {
        CooldownEvent e = new CooldownEvent();
        e.eventTime = eventStartTime + ability.cooldown;
        e.ability = ability;
        e.owner = this;
        EventQueueSingleton.queue.add(e);
    }

    public void generateEvents(float eventTime) {
        for (int i = 0; i < numberParrallelCasts; i++) {
            generateNextActiveEvent(eventTime);
        }
        foreach (Buff buff in effectList) {
            buff.generateEvents(this, eventTime);
        }
    }

    public Person() : base() {
        maxHealth = Constants.PERSON_BASE_HEALTH;
        maxMana = Constants.PERSON_BASE_MANA;
        isAlive = true;

        agro = Constants.PERSON_AGRO;

        knownAbilities.Add(new MeleeAttack("Melee Attack"));
    }

    public object Clone() {
        Person newPerson = (Person) this.MemberwiseClone();
        foreach (Ability ab in newPerson.knownAbilities) {
            ab.setPerson(newPerson);
        }
        foreach (Item item in newPerson.itemList) {
            item.owner = newPerson;
            foreach(Ability ability in item.abilityList) {
                ability.abilityTactic.person = newPerson;
                ability.personOwner = newPerson;
            }
        }
        return newPerson;
    }

    public virtual void initAbilities() {

        maxMana = basicMana * Constants.getMultiplayer(level);
        mana = maxMana;
        maxHealth = basicHealth * Constants.getMultiplayer(level);
        health = maxHealth;

        abilityList.Clear();
        effectList.Clear();
        usedAbilites.Clear();

        foreach (Ability ability in knownAbilities) {
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

        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].modificatorList.Count > 0) {
                Buff itemBuff = new Buff();
                itemBuff.setAbstractTactic(new MeleeAttackTactic());
                itemBuff.name = itemList[i].name;
                itemBuff.modificator = itemList[i].modificatorList[0];
                itemBuff.image = itemList[i].image;
                effectList.Add(itemBuff);
            }
            abilityList.AddRange(itemList[i].abilityList);
            foreach (Ability a in itemList[i].userAbilityList) {
                a.setPerson(PartiesSingleton.player);
                a.animationTime = 0.0f;
                a.playerCastCount = itemList[i].maxDurability;
                PartiesSingleton.player.abilityList.Add(a);
            }
        }

        foreach (Ability ab in abilityList) {
            ab.level = level;
            ab.initAbility();
        }
        foreach (Buff ab in effectList) {
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
        foreach (Item item in itemList) {
            total += item.powerCost + item.powerCostPerLevel * (level - 1);
        }
        return total;
    }

    public void setLevel(int level) {
        this.level = level;
        foreach (Item item in itemList) {
            item.setLevel(level);
        }
        foreach (Ability ability in knownAbilities) {
            ability.setLevel(level);
        }
    }

    public Item findItem(ItemType type) {
        foreach (Item item in itemList) {
            if (item.type == type) {
                return item;
            }
        }
        return null;
    }
}
