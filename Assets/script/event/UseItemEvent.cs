using UnityEngine;
using System.Collections;

public class UseItemEvent : BasicTargetEvent {

	public Item item;

	public override float eventStart() {
        item.durability = item.durability - 1;
        return 0.0f;
    }

    public override string toString() {
        return "Item " + item.name + " has durability [" + item.durability + "]";
    }
}
