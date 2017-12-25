using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Golem : Person{
	public Golem() : base() {
        name = "Summoned Golem";

        maxHealth = Constants.GOLEM_HEALTH;
        maxMana = Constants.GOLEM_MANA;
        healthPerLevel = Constants.GOLEM_HEALTH_PER_LEVEL;
        maxMana = Constants.GOLEM_MANA_PER_LEVEL;

        powerCost = 50;
        powerCostPerLevel = 5;

        agro = Constants.GOLEM_AGRO;

        personImage = "texture/troll";
    }
}
