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
    public float basicHealth;
    public float healthPerLevel;

    public float manaPerLevel;
    public float mana;
    public float maxMana;
    public float basicMana;

    public int numberParrallelCasts = 1;

    public String personImage = "";
    public String personModel = "";

    public AbilityTargetType enemy;
    public AbilityTargetType ally;

    public List<Ability> abilityList = new List<Ability>();
    public List<Buff> effectList = new List<Buff>();

    public List<Ability> knownAbilities = new List<Ability>();
    public List<Buff> buffList = new List<Buff>();
    public List<Item> itemList = new List<Item>();

    public List<Ability> usedAbilites = new List<Ability>();

    public Person summoner = null;
    public Place place;

    public int agro;

    public bool isAlive;
    public bool updateBuffs = true;

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

        if (ability.animation != null) {
            personController.applyEffect();
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
            if (!(ability.GetType().IsSubclassOf(typeof (ActiveBuff)))) {
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

        knownAbilities.Add(new MeleeAttack("Melee Attack"));
    }

    public object Clone() {
        try {
           Person newPerson = (Person) this.MemberwiseClone();
            foreach  (Ability ab in newPerson.abilityList) {
                ab.setPerson(newPerson);
                ab.initAbility();
            }
            foreach (Ability ab in newPerson.knownAbilities) {
                ab.setPerson(newPerson);
            }
            foreach (Buff b in newPerson.effectList) {
                b.setPerson(newPerson);
            }
            foreach (Buff b in newPerson.buffList) {
                b.setPerson(newPerson);
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

        maxMana = basicMana + level * manaPerLevel;
        mana = maxMana;
        maxHealth = basicHealth + level * healthPerLevel;
        health = maxHealth;

        abilityList.Clear();
        effectList.Clear();
        usedAbilites.Clear();

        foreach (Ability ability in knownAbilities) {
            abilityList.Add(ability);
        }

        effectList.AddRange(buffList);

        for (int i = 0; i < itemList.Count; i++) {
            foreach (Buff modificator in itemList[i].modificatorList) {
                effectList.Add(modificator);
            }
            abilityList.AddRange(itemList[i].abilityList);

            foreach (Ability ab in abilityList) {
                ab.initAbility();
            }
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
            item.level = level;
        }
        foreach (Ability ability in abilityList) {
            ability.level = level;
        }
        initAbilities();
    }
}
