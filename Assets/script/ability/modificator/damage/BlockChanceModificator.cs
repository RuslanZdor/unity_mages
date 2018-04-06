using UnityEngine;

public class BlockChanceModificator : AbstractModificator{

	public float chance;

    public BlockChanceModificator(float chance) {
        this.chance =  chance;
    }

 
	public override void updateGettingDamage(Ability ability) {
        if (chance > 0)  {
			if (Random.Range(0, 100) <= chance) {
				ability.effectList.FindAll(
                        effect =>
                        !effect.attribures.Contains(EffectAttribures.BLOCK))
                    .ForEach(effect => {
                        effect.value = effect.value / 2;
                        effect.attribures.Add(EffectAttribures.BLOCK);
                });
            }
        }

    }
}
