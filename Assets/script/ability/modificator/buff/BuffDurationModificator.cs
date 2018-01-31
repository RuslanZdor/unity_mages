using UnityEngine;
using System.Collections;

public class BuffDurationModificator : AbstractModificator{

    public float value;

    public BuffDurationModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateMakingBuff(Ability ability) {
        Buff buff = (Buff) ability;
        buff.duration = (int) buff.duration * (100 + value) / 100;
    }
}
