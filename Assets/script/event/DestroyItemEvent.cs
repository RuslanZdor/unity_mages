using UnityEngine;
using System.Collections;

public class DestroyItemEvent : BasicTargetEvent {

	public Item item;

	public override float eventStart() {
        owner.itemList.Remove(item);

        return 0.0f;
    }

    public override string toString() {
        return "Item " + item.name + " was destroyed";
    }
}
