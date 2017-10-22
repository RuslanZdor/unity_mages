using UnityEngine;
using System.Collections;

public class SummonGolem : SummonAbility {

	public SummonGolem(Person person) : base(person, new SummonCastTactic(3)) {

        Person summon = new Golem();
        summon.ally = person.ally;
        summon.enemy = AbilityTargetType.FRIEND;
		getAbilityTactic().summon = summon;
		timeCast = 1.0f;
		targetType = person.ally;
		manaCost = 1;

		targetTactic = new ItselfTargetTactic(person);

		SummonEffect effect = new SummonEffect();
		effect.person = summon;
		effect.duration = 5;
		effectList.Add(effect);
	}
}

