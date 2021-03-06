﻿using script;

public class SummonGolem : SummonAbility {

	public SummonGolem()
	{
        var summon = XMLFactory.loadPerson("configs/monsters/golems/golem");
        getAbilityTactic().summon = summon;
        timeCast = 1.0f;
		manaCost = 1;
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_60");
        name = "Summon Golem";
        type = "activeAbility";

        targetTactic = new ItselfTargetTactic();

		var effect = new SummonEffect();
		effect.person = summon;
		effect.duration = 0;
		effectList.Add(effect);
	}
}

