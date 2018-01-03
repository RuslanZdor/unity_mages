using UnityEngine;
using System.Collections;

public class PassiveRegeneration : ActiveBuff {
	public PassiveRegeneration(int value) : base(new MeleeAttackTactic()) {
        name = "Passive Regeneration";
        
        modificator = new AbstractModificator();
        targetType = AbilityTargetType.FRIEND;
        targetTactic = new ItselfTargetTactic();
        timeCast = 1;

        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_0");

        AbstractAbilityEffect effect = new HealAbilityEffect();
        effect.targetsNumber = 1;
        effect.valueGenerator = new ConstantValueGenerator(value);
        effectList.Add(effect);

    }
}