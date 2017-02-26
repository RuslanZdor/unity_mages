using UnityEngine;
using System.Collections;

public class Weakness : Buff {

	public Weakness(Person person) :  base(person, new DamageSpellCastTactic(3)) {
		name = "Weakness";
		timeCast = 1.0;
		targetType = person.enemy;
		manaCost = 1;
		duration = 5;

		AddBuffEffect effect = new AddBuffEffect();
		effect.targetsNumber = 1;
		effect.buff = this;
		effectList.Add(effect);

		targetTactic = new WithoutBuffTactic(this);

		modificator = new IncreaseDamageModificator(-30);
	}
}

