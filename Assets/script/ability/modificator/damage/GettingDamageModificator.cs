using UnityEngine;
using System.Collections;

public class GettingDamageModificator : AbstractModificator{

    public float value;

    public GettingDamageModificator(float chance) {
        this.value =  chance;
    }
		
	public override void updateGettingDamage(Ability ability) {
		ability.effectList.ForEach ((AbstractAbilityEffect effect) => 
			effect.value = (int) (effect.value * (100 - value) / 100));
    }
}
