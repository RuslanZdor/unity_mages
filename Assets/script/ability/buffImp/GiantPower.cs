using UnityEngine;
using System.Collections;

public class GiantPower : Buff {

	public GiantPower(Person person) : base(person, new DamageSpellCastTactic(3)) {
		name = "Giant Power";
		timeCast = 1.0f;
		targetType = AbilityTargetType.FRIEND;
		manaCost = 1;
		duration = 5;

		AddBuffEffect effect = new AddBuffEffect();
		effect.targetsNumber = 1;
		effect.buff = this;
		effectList.Add(effect);

		targetTactic = new WithoutBuffTactic(this);

		modificator = new IncreaseDamageModificator(50);
	}
}

