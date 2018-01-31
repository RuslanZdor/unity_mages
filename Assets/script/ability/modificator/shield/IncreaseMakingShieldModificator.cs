using UnityEngine;
using System.Collections;

public class IncreaseMakingShieldModificator : AbstractModificator{

    public float value;

    public IncreaseMakingShieldModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateMakingShield(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
