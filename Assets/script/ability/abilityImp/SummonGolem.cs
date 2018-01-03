using UnityEngine;
using System.Collections;

public class SummonGolem : SummonAbility {

	public SummonGolem(AbstractTactic tactic) : base(tactic) {

        Person summon = new Golem();
		getAbilityTactic().summon = summon;
        name = "Summon Golem";
        timeCast = 1.0f;
		manaCost = 1;
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_60");

        targetTactic = new ItselfTargetTactic();

		SummonEffect effect = new SummonEffect();
		effect.person = summon;
		effect.duration = 0;
		effectList.Add(effect);
	}
}

