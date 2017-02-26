using UnityEngine;
using System.Collections;

public class ElementsDamageModificator : AbstractModificator{

    public EffectAttribures value;

    public ElementsDamageModificator(EffectAttribures value) {
        this.value =  value;
    }

	public override void updateGettingDamage(Ability ability) {
		ability.effectList.ForEach((AbstractAbilityEffect effect) => {
			foreach (EffectAttribures attr in effect.attribures) {
                 effect.value = (int) (effect.value * ElementalMiultiplicators.getMultiplicator(attr, value));
             }
         });
    }
}
