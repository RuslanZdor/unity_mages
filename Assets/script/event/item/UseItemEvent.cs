using UnityEngine;
using System.Collections;

public class UseItemEvent : BasicTargetEvent {

	public Item item;

	public override float eventStart() {
        item.durability = item.durability - 1;
        logEvent("Item " + item.name + " has durability [" + item.durability + "]");
        return 0.0f;
    }
}
