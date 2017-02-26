using UnityEngine;
using System.Collections;

public class DamageSpellCastTactic :  AbstractTactic{

	public override int getPriority() {
        if (person.mana > ability.manaCost) {
            return defaultPriority;
        }
        return 0;
    }

    public DamageSpellCastTactic(int defaultPriority) {
        this.defaultPriority = defaultPriority;
    }
}
