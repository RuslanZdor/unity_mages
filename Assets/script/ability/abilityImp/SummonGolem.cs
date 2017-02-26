using UnityEngine;
using System.Collections;

public class SummonGolem : SummonAbility {

	public SummonGolem(Person person) : base(person, new SummonCastTactic(3)) {
		Person summon = new Golem(person.ally, person.enemy);
		getAbilityTactic().summon = summon;
		name = "Summon Golem";
		timeCast = 1.0;
		targetType = person.ally;
		manaCost = 1;

		targetTactic = new ItselfTargetTactic(person);

		SummonEffect effect = new SummonEffect();
		effect.person = summon;
		effect.duration = 5;
		effectList.Add(effect);
	}
}

