using UnityEngine;
using System.Collections;

public class PassiveRegeneration : ActiveBuff {
	public PassiveRegeneration(Person person, int value) : base(person, new MeleeAttackTactic()) {
        name = "Passive Regeneration";

        modificator = new AbstractModificator();
        targetType = person.ally;
        targetTactic = new ItselfTargetTactic(person);
        timeCast = 1;

        AbstractAbilityEffect effect = new HealAbilityEffect();
        effect.targetsNumber = 1;
        effect.valueGenerator = new ConstantValueGenerator(value);
        effectList.Add(effect);

    }
}