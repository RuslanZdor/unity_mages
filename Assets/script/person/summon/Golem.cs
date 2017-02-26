using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Golem : Person{
	public Golem(AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy) {
        init();
    }

	protected override void init() {
        name = "Summoned Golem";

        maxHealth = Constants.GOLEM_HEALTH;
        maxMana = Constants.GOLEM_MANA;
        healthPerLevel = Constants.GOLEM_HEALTH_PER_LEVEL;
        maxMana = Constants.GOLEM_MANA_PER_LEVEL;

        agro = Constants.GOLEM_AGRO;

		base.init();

    }
}
