using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebuffMage : BaseMage {
	public DebuffMage() : base()  {
        abilityList.Add(new Weakness(this));
    }
}
