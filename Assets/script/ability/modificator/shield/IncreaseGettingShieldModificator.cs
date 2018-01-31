using UnityEngine;
using System.Collections;

public class IncreaseGettingShieldModificator : AbstractModificator{

    public float value;

    public IncreaseGettingShieldModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateGettingShield(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
