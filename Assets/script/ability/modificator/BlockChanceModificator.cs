using UnityEngine;
using System.Collections;

public class BlockChanceModificator : AbstractModificator{

	public double chance;

    public BlockChanceModificator(double chance) {
        this.chance =  chance;
    }

 
	public override void updateGettingDamage(Ability ability) {
        if (chance > 0)  {
			if (Random.Range(0, 100) <= chance) {
				ability.effectList.ForEach((AbstractAbilityEffect effect) => {
                    effect.value = effect.value / 2;
                    effect.attribures.Add(EffectAttribures.BLOCK);
                });
            }
        }

    }
}