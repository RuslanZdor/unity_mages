using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Person : MonoBehaviour, ICloneable {

    public int id;
    public string name;
    public int level;

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
    public List<Item> itemList = new List<Item>();

    public List<Ability> usedAbilites = new List<Ability>();

    public Person summoner = null;

    public int agro;

    public bool isAlive;
    public bool updateBuffs = false;

    public String personImage = "";

    void Update() {
        if (health <= 0) {
            isAlive = false;
        }
    }

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

    public double damage(Ability ability) {
        double resultValue = health;
        if (isAlive) {
            for (int i = 0; i < ability.effectList.Count; i++) {
                health -= ability.effectList[i].value;
            }

            if (health < 0) {
                health = 0;
            }
        }
        return resultValue - health;
    }

    public double heal(Ability ability) {
        double resultValue = health;
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

    public void eventStart(Ability ability) {
        if (isAlive) {
            ability.eventStart();
            if (!(ability.GetType() == typeof(ActiveBuff))) {
                usedAbilites.Remove(ability);
                generateNextActiveEvent();
            }
        }
    }

    public void generateNextActiveEvent() {
        GenerateAbilityEvent e = new GenerateAbilityEvent();
        e.eventTime = EventQueueSingleton.queue.currentTime;
        e.owner = this;
        EventQueueSingleton.queue.add(e);
    }

    public void generateEvents() {
        for (int i = 0; i < numberParrallelCasts; i++) {
            generateNextActiveEvent();
        }
        foreach (Buff buff in effectList) {
            buff.generateEvents(this);
        }
    }

    public Person() : base() {
        maxHealth = Constants.PERSON_BASE_HEALTH;
        maxMana = Constants.PERSON_BASE_MANA;
        healthPerLevel = Constants.PERSON_HEALTH_PER_LEVEL;
        manaPerLevel = Constants.PERSON_MANA_PER_LEVEL;
        isAlive = true;

        agro = Constants.PERSON_AGRO;
    }

    public object Clone() {
        try {
            return this.MemberwiseClone();
        } catch (Exception e) {
            Debug.Log(e);
        }
        return null;
    }

    protected virtual void init() {
        maxMana = maxMana + level * manaPerLevel;
        mana = maxMana;

        maxHealth = maxHealth + level * healthPerLevel;
        health = maxHealth;

        abilityList.Add(new MeleeAttack(this, "Melee Attack"));

        for (int i = 0; i < itemList.Count; i++) {
            Buff b = new Buff(this, new DamageSpellCastTactic(1));
            foreach (AbstractModificator modificator in itemList[i].modificatorList) {
                b.modificator = modificator;
                b.name = itemList.GetType().FullName + ":" + modificator.GetType().FullName;
                effectList.Add(b);
            }

            abilityList.AddRange(itemList[i].abilityList);
        }
    }

    public void updateAgro(int value) {
        if (value > agro && value <= agro * 3) {
            agro = value;
        }
    }
}
