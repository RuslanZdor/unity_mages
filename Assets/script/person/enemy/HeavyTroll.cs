using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeavyTroll : Troll {
	public HeavyTroll() : base() {
        init();
    }

	protected override void init() {
		base.init();
        effectList.Add(new PassiveBlockChance(this, 50));
        itemList.Add(new Shield(this));
    }

}
