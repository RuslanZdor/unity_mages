using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeavyTroll : Troll {
	public HeavyTroll() : base() {
        effectList.Add(new PassiveBlockChance(this, 50));
        itemList.Add(new Shield(this));
    }
}
