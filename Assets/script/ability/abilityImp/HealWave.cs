using UnityEngine;
using System.Collections;

public class HealWave : Ability {

	public HealWave(Person person) : base(person, new DamageSpellCastTactic(3)) {
		name = "Heal Wave";
		timeCast = 2.0f;
		manaCost = 1;
		targetType = AbilityTargetType.FRIEND;

        image = Resources.Load<Sprite>("texture/Skills/healwave");


        AbstractAbilityEffect effect = new HealAbilityEffect();
		effect.targetsNumber = 1;
		effect.valueGenerator = new ConstantValueGenerator(10);
		effect.attribures.Add(EffectAttribures.LIGHT);

		effectList.Add(effect);

		targetTactic = new DamagedTargetTactic();
	}
}

