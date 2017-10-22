using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Troll : Person {

	public Troll() : base() {
    }

	protected override void init() {
        maxHealth = Constants.TROLL_HEALTH;
        maxMana = Constants.TROLL_MANA;
        healthPerLevel = Constants.TROLL_HEALTH_PER_LEVEL;
        maxMana = Constants.TROLL_MANA_PER_LEVEL;

        effectList.Add(new PassiveRegeneration(this, 1));
        effectList.Add(new PassiveElementalModificator(this, EffectAttribures.PHYSICS));

        itemList.Add(new TrollMace(this));

        personImage = "texture/troll";

        base.init();
    }
}
