using UnityEngine;

public class DodgeChanceModificator : AbstractModificator{

    public float chance;

    public DodgeChanceModificator(float chance) {
        this.chance =  chance;
    }
		
	public override void updateGettingDamage(Ability ability) {
        if (chance > 0)  {
			if (Random.Range(0,100) <= chance) {
                ability.effectList.FindAll(
                        effect =>
                        !effect.attribures.Contains(EffectAttribures.DODGE))
                    .ForEach(effect => {
					    effect.value = 0;
                        effect.attribures.Add(EffectAttribures.DODGE);
                });
            }
        }

    }
}
