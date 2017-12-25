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
    public float maxHealth;

    public float healthPerLevel;
    public float manaPerLevel;

    public float mana;
    public float maxMana;

    public int numberParrallelCasts = 1;

    public AbilityTargetType enemy;
    public AbilityTargetType ally;

    public List<Ability> abilityList = new List<Ability>();
    public List<Buff> effectList = new List<Buff>();

    public List<Ability> knownAbilities = new List<Ability>();
    public List<Item> itemList = new List<Item>();

    public List<Ability> usedAbilites = new List<Ability>();

    public Person summoner = null;
    public Place place;

    public int agro;

    public bool isAlive;
    public bool updateBuffs = true;

    public String personImage = "";

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
        float resultValue = health;
        if (isAlive) {
            for (int i = 0; i < ability.effectList.Count; i++) {
                health -= ability.effectList[i].value;
            }

            if (health < 0) {
                health = 0;
                isAlive = false;
            }
        }
        return resultValue - health;
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

    public float eventStart(Ability ability, float eventStartTime) {
        float time = 0.0f;
        if (isAlive) {
            if (!(ability.GetType() == typeof(ActiveBuff))) {
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
        healthPerLevel = Constants.PERSON_HEALTH_PER_LEVEL;
        manaPerLevel = Constants.PERSON_MANA_PER_LEVEL;
        isAlive = true;

        agro = Constants.PERSON_AGRO;

        knownAbilities.Add(new MeleeAttack(this, "Melee Attack"));
    }

    public object Clone() {
        try {
           Person newPerson = (Person) this.MemberwiseClone();
            foreach  (Ability ab in newPerson.abilityList) {
                ab.abilityTactic.person = newPerson;
                ab.personOwner = newPerson;
            }
            foreach (Ability ab in newPerson.knownAbilities) {
                ab.abilityTactic.person = newPerson;
                ab.personOwner = newPerson;
            }
            foreach (Buff b in newPerson.effectList) {
                b.personOwner = newPerson;
            }
            foreach (Item item in newPerson.itemList) {
                item.owner = newPerson;
                foreach(Ability ability in item.abilityList) {
                    ability.abilityTactic.person = newPerson;
                    ability.personOwner = newPerson;
                }
            }

            return newPerson;
        } catch (Exception e) {
            Debug.LogError(e);
        }
        return null;
    }

    public virtual void initAbilities() {

        maxMana = maxMana + level * manaPerLevel;
        mana = maxMana;
        maxHealth = maxHealth + level * healthPerLevel;
        health = maxHealth;

        abilityList.Clear();
        effectList.Clear();
        usedAbilites.Clear();

        foreach (Ability ability in knownAbilities) {
            abilityList.Add(ability);
        }

        for (int i = 0; i < itemList.Count; i++) {
            Buff b = new Buff(this, new DamageSpellCastTactic(1));
            foreach (AbstractModificator modificator in itemList[i].modificatorList) {
                b.modificator = modificator;
                b.name = itemList[i].GetType().FullName + ":" + modificator.GetType().FullName;
                effectList.Add(b);
            }
            abilityList.AddRange(itemList[i].abilityList);
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
        return powerCost + powerCostPerLevel * (level - 1);
    }
}
