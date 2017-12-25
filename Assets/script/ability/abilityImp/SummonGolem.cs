using UnityEngine;
using System.Collections;

public class SummonGolem : SummonAbility {

	public SummonGolem(Person person, AbstractTactic tactic) : base(person, tactic) {

        Person summon = new Golem();
		getAbilityTactic().summon = summon;
        name = "Summon Golem";
        timeCast = 1.0f;
		targetType = person.ally;
		manaCost = 1;
        image = Resources.Load<Sprite>("texture/Skills/golem");

        this.person = summon;
 
        targetTactic = new ItselfTargetTactic(person);

		SummonEffect effect = new SummonEffect();
		effect.person = summon;
		effect.duration = 0;
		effectList.Add(effect);
	}
}

