using UnityEngine;
using System.Collections;

public class CooldownEvent : BasicTargetEvent {
	public override float eventStart() {
        owner.usedAbilites.Remove(ability);
        return 0.0f;
    }

    public override string toString() {
        return "";  //owner.name + " generate ability";
    }
}
