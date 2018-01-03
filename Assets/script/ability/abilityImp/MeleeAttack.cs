using UnityEngine;
using System.Collections;

public class MeleeAttack : DamageAbility {

	public MeleeAttack(string n) :  base(new MeleeAttackTactic(1)){
		name = n;
		timeCast = Constants.PERSON_MELEE_ATTACK_SPEED;
		targetType = AbilityTargetType.ENEMY;
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_2");

        minDamage = 1;
        maxDamage = 1;

        targetTactic = new RandomTargetTactic();
    }
}

