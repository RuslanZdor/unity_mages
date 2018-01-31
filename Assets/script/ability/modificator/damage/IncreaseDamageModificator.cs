using UnityEngine;
using System.Collections;

public class IncreaseDamageModificator : AbstractModificator{

    public float value;

    public IncreaseDamageModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateMakingDamage(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
