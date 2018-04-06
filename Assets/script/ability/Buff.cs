using System;
using UnityEngine;

public class Buff : Ability {

	public AbstractModificator modificator;
	public float duration;

    public Buff clone() {
        Buff cl = null;
        try {
			cl = (Buff) MemberwiseClone();
		} catch (Exception e) {
			Debug.LogError(e);
        }

        return cl;
    }

    public bool Equals(Buff obj) {
        return name.Equals(obj.name);
    }

    public int compareTo(Buff o)
    {
        if (priority < o.priority) {
            return -1;
        }
        return 1;
    }

    public override void initAbility() {
        if (modificator != null) {
            modificator.updateLevel(level);
        }
        base.initAbility();
    }
}
