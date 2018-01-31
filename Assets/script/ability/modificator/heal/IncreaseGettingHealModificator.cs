using UnityEngine;
using System.Collections;

public class IncreaseGettingHealModificator : AbstractModificator{

    public float value;

    public IncreaseGettingHealModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateGettingHeal(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
