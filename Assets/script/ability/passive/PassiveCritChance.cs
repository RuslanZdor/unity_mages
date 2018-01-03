using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveCritChance : Buff {
	public PassiveCritChance(float value) : base(new DamageSpellCastTactic(3)){
        name ="Passive Crit Change";
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_40");
        modificator = new CritChanceModificator(value);
    }
}