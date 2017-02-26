using UnityEngine;
using System.Collections;

public class Shield : Item {
	public Shield(Person person) : base(person) {
        name = Constants.ITEM_SHIELD_NAME;
        cost = Constants.ITEM_SHIELD_COST;
        durability = Constants.ITEM_SHIELD_USABLE_COUNT;
        modificatorList.Add(new BlockChanceModificator(Constants.ITEM_SHIELD_BLOCK_CHANCE));
    }
}
