using UnityEngine;
using System.Collections;

public class AddBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override void eventStart() {
        target.effectList.Add(buff);
		Debug.Log(eventTime + " : " + owner.name + " cast " + buff.name + " to " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]");

        if (owner.enemy.Equals(target.ally)) {
            owner.updateAgro(owner.agro + 1);
        }
    }
}
