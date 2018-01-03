using UnityEngine;
using System.Collections;

public class Weakness : Buff {

	public Weakness() :  base(new DamageSpellCastTactic(3)) {
		name = "Weakness";
		timeCast = 1.0f;
		targetType = AbilityTargetType.ENEMY;
		manaCost = 1;
		duration = 5;
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_26");

        AddBuffEffect effect = new AddBuffEffect();
		effect.targetsNumber = 1;
		effect.buff = this;
		effectList.Add(effect);

		targetTactic = new WithoutBuffTactic(this);

		modificator = new IncreaseDamageModificator(-30);
	}
}

