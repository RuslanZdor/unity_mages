using UnityEngine;
using System.Collections;

public class MeleeAttack : Ability {

	public MeleeAttack(string n) :  base(){
		name = n;
		timeCast = Constants.PERSON_MELEE_ATTACK_SPEED;
		targetType = AbilityTargetType.ENEMY;
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_2");
        type = "activeAbility";
        setAbstractTactic(new MeleeAttackTactic(1));

        AbstractAbilityEffect effect = new DamageAbilityEffect();
        effect.targetsNumber = 1;
        effect.valueGenerator = new ConstantValueGenerator(1);
        effectList.Add(effect);

        targetTactic = new RandomTargetTactic();
    }
}

