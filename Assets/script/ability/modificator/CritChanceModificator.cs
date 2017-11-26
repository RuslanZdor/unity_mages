using UnityEngine;
using System.Collections;

public class CritChanceModificator : AbstractModificator{

	public float critChange;

    public CritChanceModificator(float critChange) {
        this.critChange =  critChange;
    }

	public override void updateMakingDamage(Ability ability) {
        if (critChange > 0)  {
			if (Random.Range(0, 100) <= critChange) {
                ability.effectList.ForEach(
					(AbstractAbilityEffect effect) => {
							effect.value = effect.value * 2;
                            effect.attribures.Add(EffectAttribures.CRIT);
                        }
                );
            }
        }
    }
}
