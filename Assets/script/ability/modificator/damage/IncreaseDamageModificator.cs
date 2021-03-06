public class IncreaseDamageModificator : AbstractModificator{

    public float value;

    public IncreaseDamageModificator(float chance) {
        value =  chance;
    }
		
	public override void updateMakingDamage(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
