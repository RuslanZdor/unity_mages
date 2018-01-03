using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBall : Ability {
	public FireBall() : base(new DamageSpellCastTactic(3)) {
		name  = "Fireball";
		timeCast = 1.5f;
		manaCost = 1;
        cooldown = 3.0f;
		targetType = AbilityTargetType.ENEMY;
        animation = "animation/meleeAttackAnimation";
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_14");

        AbstractAbilityEffect effect = new DamageAbilityEffect();
		effect.targetsNumber = 1;
		effect.valueGenerator = new RangeValueGenerator(10,15);
		effect.attribures.Add(EffectAttribures.FIRE);

		effectList.Add(effect);

		targetTactic = new RandomTargetTactic();
	}
}
