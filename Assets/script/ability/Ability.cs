using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ability : ICloneable {
    public String name;

    public float timeCast;
    public float cooldown;
	public float manaCost;
    
	public List<AbstractAbilityEffect> effectList = new List<AbstractAbilityEffect>();
	public AbilityTargetType targetType;
	public AbstractTactic abilityTactic;
	public AbstractTargetTactic targetTactic;

    public float animationTIme = 1.0f;
    public String animation;

	public Person personOwner;

    public virtual float eventStart() {
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
                    effect.applyEffect(personOwner, target);
                }
            }

            if (effectList.FindAll(
                (AbstractAbilityEffect eff) =>
                eff.attribures.FindAll(
                    (EffectAttribures attr) => attr == EffectAttribures.MELEE_ATTACK
                ).Count > 0
            ).Count > 0) {
                personOwner.meleeAttackAbility();
            } else {
                personOwner.castAbility();
            }


            return animationTIme;
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

    public void generateEvents(Person person) {
        if (this.effectList.Count > 0) {
            Event e = new Event();
            e.eventTime = EventQueueSingleton.queue.nextEventTime + timeCast;
            e.ability = this;
            e.owner = person;
            EventQueueSingleton.queue.add(e);
        }
    }

    public Ability(Person person, AbstractTactic tactic) {
        abilityTactic = tactic;
        abilityTactic.person = person;
        abilityTactic.ability = this;
        personOwner = person;
    }

	public object Clone() {
		return this.MemberwiseClone();
	}
		
}
