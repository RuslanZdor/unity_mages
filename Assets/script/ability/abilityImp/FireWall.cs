using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWall : Ability {
	public FireWall(Person person) : base(person, new DamageSpellCastTactic(3)) {
		name  = "Fireball";
		timeCast = 1.5f;
		manaCost = 1;
        cooldown = 3.0f;
		targetType = AbilityTargetType.ENEMY;
        animation = "animation/meleeAttackAnimation";
        image = Resources.Load<Sprite>("texture/Skills/firewall");


        AbstractAbilityEffect effect = new RowDamageAbilityEffect();
		effect.targetsNumber = 1;
		effect.valueGenerator = new RangeValueGenerator(5,10);
		effect.attribures.Add(EffectAttribures.FIRE);

		effectList.Add(effect);

		targetTactic = new RandomTargetTactic();
	}
}
