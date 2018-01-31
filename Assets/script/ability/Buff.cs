using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Buff : Ability {

	public AbstractModificator modificator;
	public float duration;

    public Buff clone() {
        Buff cl = null;
        try {
			cl = (Buff) this.MemberwiseClone();
		} catch (Exception e) {
			Debug.LogError(e);
        }

        return cl;
    }

    public bool Equals(Buff obj) {
        return name.Equals(((Buff) obj).name);
    }

    public int compareTo(Buff o) {
        if (priority < o.priority) {
            return -1;
        }else {
            return 1;
        }
    }

    public override void initAbility() {
        if (modificator != null) {
            modificator.updateLevel(level);
        }
        base.initAbility();
    }
}
