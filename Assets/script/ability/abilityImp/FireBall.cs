using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBall : Ability {
	public FireBall(Person person) : base(person, new DamageSpellCastTactic(3)) {
		name  = "Fireball";
		timeCast = 1.5f;
		manaCost = 1;
		targetType = AbilityTargetType.ENEMY;

		AbstractAbilityEffect effect = new DamageAbilityEffect();
		effect.targetsNumber = 1;
		effect.valueGenerator = new RangeValueGenerator(5,15);
		effect.attribures.Add(EffectAttribures.FIRE);

		effectList.Add(effect);

		targetTactic = new RandomTargetTactic();
	}
}
