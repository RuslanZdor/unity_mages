using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMage : BaseMage {
	public FireMage() : base() {
        knownAbilities.AddRange(XMLFactory.loadSkillSet("configs/skillSet/heroes/fire_mage"));
        itemList.Add(XMLFactory.loadItem("configs/items/activeGems/heal_gem"));
    }
}
