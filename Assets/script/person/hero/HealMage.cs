using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealMage : BaseMage {
	public HealMage () : base() {
        knownAbilities.Add(XMLFactory.loadAbility("configs/abilities/heal/healwave"));
        itemList.Add(XMLFactory.loadItem("configs/items/activeGems/fire_gem"));
    }
}
