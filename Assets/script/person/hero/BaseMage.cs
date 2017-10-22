using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseMage : Person {
	public BaseMage() : base() {
   }

	protected override void init() {
        maxHealth = Constants.BASE_MAGE_HEALTH;
        maxMana = Constants.BASE_MAGE_MANA;
        healthPerLevel = Constants.BASE_MAGE_HEALTH_PER_LEVEL;
        manaPerLevel = Constants.BASE_MAGE_MANA_PER_LEVEL;

        itemList.Add(new MageStaff(this));

        personImage = "texture/model";

        base.init();
    }
}
