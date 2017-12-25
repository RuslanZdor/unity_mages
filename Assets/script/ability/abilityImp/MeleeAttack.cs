using UnityEngine;
using System.Collections;

public class MeleeAttack : Ability {

	public MeleeAttack(Person person, string n) :  base(person, new MeleeAttackTactic(1)){
		name = n;
		timeCast = Constants.PERSON_MELEE_ATTACK_SPEED;
		targetType = AbilityTargetType.ENEMY;
        image = Resources.Load<Sprite>("texture/Skills/handAttack");

        AbstractAbilityEffect effect = new DamageAbilityEffect();
		effect.targetsNumber = 1;
		effect.valueGenerator = new ConstantValueGenerator(Constants.PERSON_MELEE_ATTACK_DAMAGE);
		effect.attribures.Add(EffectAttribures.MELEE_ATTACK);

		effectList.Add(effect);

        targetTactic = new RandomTargetTactic();
	}
}

