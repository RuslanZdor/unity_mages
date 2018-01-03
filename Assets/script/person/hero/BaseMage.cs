using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseMage : Person {
	public BaseMage() : base() {
        personImage = "texture/model";
        personModel = "models/persons/BasicMageModel";

        basicHealth = Constants.BASE_MAGE_HEALTH;
        basicMana = Constants.BASE_MAGE_MANA;
        healthPerLevel = Constants.BASE_MAGE_HEALTH_PER_LEVEL;
        manaPerLevel = Constants.BASE_MAGE_MANA_PER_LEVEL;

        itemList.Add(XMLFactory.loadItem("configs/items/weapons/mage_stuff"));

        itemList.Add(XMLFactory.loadItem("configs/items/shields/shield"));
    }
}
