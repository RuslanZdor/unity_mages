using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseMage : Person {
	public BaseMage() : base() {
        personImage = "texture/model";
        personModel = "models/persons/BasicMageModel";

        shield = 20;

        setLevel(10);

        basicHealth = Constants.BASE_MAGE_HEALTH;
        basicMana = Constants.BASE_MAGE_MANA;

        itemList.Add(XMLFactory.loadItem("configs/items/weapons/mage_stuff"));

        itemList.Add(XMLFactory.loadItem("configs/items/shields/shield"));

        isActive = true;
    }
}
