using UnityEngine;
using System.Collections;

public class DestroyItemEvent : BasicTargetEvent {

	public Item item;

	public override float eventStart() {
        owner.itemList.Remove(item);
        logEvent(" item was destroied " + item.name);
        return 0.0f;
    }
}
