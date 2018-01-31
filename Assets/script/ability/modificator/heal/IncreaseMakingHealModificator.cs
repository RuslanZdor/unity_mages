using UnityEngine;
using System.Collections;

public class IncreaseMakingHealModificator : AbstractModificator{

    public float value;

    public IncreaseMakingHealModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateMakingHeal(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
