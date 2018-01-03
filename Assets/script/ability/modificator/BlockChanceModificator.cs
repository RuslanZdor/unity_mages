using UnityEngine;
using System.Collections;

public class BlockChanceModificator : AbstractModificator{

	public float chance;

    public BlockChanceModificator(float chance) {
        this.chance =  chance;
    }

 
	public override void updateGettingDamage(Ability ability) {
        if (chance > 0)  {
			if (Random.Range(0, 100) <= chance) {
				ability.effectList.FindAll(
                        (AbstractAbilityEffect effect) =>
                        !effect.attribures.Contains(EffectAttribures.BLOCK))
                    .ForEach((AbstractAbilityEffect effect) => {
                        effect.value = effect.value / 2;
                        effect.attribures.Add(EffectAttribures.BLOCK);
                });
            }
        }

    }
}
