using UnityEngine;
using System.Collections;

public class IncreaseMeleeDamageModificator : AbstractModificator{
    public float value;

    public IncreaseMeleeDamageModificator(float value) {
        this.value =  value;
    }

	public override void updateMakingDamage(Ability ability) {
		ability.effectList.FindAll((AbstractAbilityEffect effect) => 
			effect.attribures.Contains(EffectAttribures.MELEE_ATTACK)).ForEach((AbstractAbilityEffect effect) =>
				effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
