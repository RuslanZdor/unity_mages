using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ability : ICloneable {
    public string name;
    public string type;
    public string resource;

    public float timeCast;
    public float cooldown;
	public float manaCost;
    public int level;
    public int priority = 1;
    public int playerCastCount = 0;

    public int requiredLevel = 1;
    public Vector2 position;
    public Boolean isActive = false;
    
	public List<AbstractAbilityEffect> effectList = new List<AbstractAbilityEffect>();
	public AbilityTargetType targetType;
	public AbstractTactic abilityTactic;
	public AbstractTargetTactic targetTactic;

    public float animationTime = 0.5f;
    public String animation;
    public Sprite image;

	public Person personOwner;

    public virtual float eventStart(float startTime) {
        if (canUse()) {
            personOwner.mana = personOwner.mana - manaCost;
			foreach (AbstractAbilityEffect effect in effectList) {
                AbilityTargetType targetParty = AbilityTargetType.ENEMY;
                if (targetType == AbilityTargetType.ENEMY) {
                    targetParty = personOwner.enemy;
                }else {
                    targetParty = personOwner.ally;
                }
                Party party = PartiesSingleton.getParty(targetParty);
                List<Person> targets = targetTactic.getTargets(party, effect.targetsNumber, this);
				foreach (Person target in targets) {
                    effect.applyEffect(personOwner, target, startTime, this);
                }
            }

            if (animationTime > 0) {
                if (effectList.FindAll(
                    (AbstractAbilityEffect eff) =>
                    eff.attribures.FindAll(
                        (EffectAttribures attr) => attr == EffectAttribures.MELEE_ATTACK
                    ).Count > 0
                ).Count > 0) {
                    personOwner.personController.meleeAttackAbility();
                } else {
                    personOwner.personController.castAbility();
                }
            }


            return animationTime;
        }
        return 0.0f;
    }

    public bool canUse() {
        if (manaCost == 0 || personOwner.mana >= manaCost) {
            return true;
        }else {
            return false;
        }
    }

    public void generateEvents(Person person, float currentTime) {
        if (this.effectList.Count > 0) {
            Event e = new Event();
            e.eventTime = currentTime + timeCast;
            e.ability = (Ability) this.Clone();
            e.owner = person;
            e.eventDuration = animationTime;
            EventQueueSingleton.queue.add(e);
        }
    }

    public void setAbstractTactic(AbstractTactic tactic) {
        abilityTactic = tactic;
        abilityTactic.ability = this;
    }

	public object Clone() {
        Ability ab = (Ability) this.MemberwiseClone();
        List<AbstractAbilityEffect> newList = new List<AbstractAbilityEffect>();
        foreach (AbstractAbilityEffect aae in ab.effectList) {
            newList.Add((AbstractAbilityEffect) aae.Clone());
        }
        ab.effectList = newList;
        return ab;
	}
		
    public virtual void setPerson(Person person) {
        personOwner = person;
        abilityTactic.person = person;
    }

    public virtual void initAbility() {
        foreach (AbstractAbilityEffect effect in effectList) {
            effect.updateLevel(level);
        }
    }

    public bool hasAttribute(EffectAttribures attribute) {
        return effectList.FindAll((AbstractAbilityEffect effect) =>
                effect.attribures.Contains(attribute)).Count > 0;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void setRequiredLevel(int level) {
        requiredLevel = level;
        abilityTactic.defaultPriority = 10 + requiredLevel;
    }
}
