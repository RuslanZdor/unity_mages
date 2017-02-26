using UnityEngine;
using System.Collections;

public class UseItemEvent : BasicTargetEvent {

	public Item item;

	public override void eventStart() {
        item.durability = item.durability - 1;
		Debug.Log("Item " + item.name + " has durability [" + item.durability + "]");
    }
}
