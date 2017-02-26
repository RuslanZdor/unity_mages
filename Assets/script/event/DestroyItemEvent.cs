using UnityEngine;
using System.Collections;

public class DestroyItemEvent : BasicTargetEvent {

	public Item item;

	public override void eventStart() {
		Debug.Log("Item " + item.name + " was destroyed");
        owner.itemList.Remove(item);
    }
}
