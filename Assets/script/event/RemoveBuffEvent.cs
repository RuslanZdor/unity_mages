using UnityEngine;
using System.Collections;

public class RemoveBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override void eventStart() {
        target.effectList.Remove(buff);
		Debug.Log(eventTime + " : " + " remove " + buff.name + " from " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]");
    }
}
