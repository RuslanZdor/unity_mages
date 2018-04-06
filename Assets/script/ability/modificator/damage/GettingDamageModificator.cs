public class GettingDamageModificator : AbstractModificator{

    public float value;

    public GettingDamageModificator(float chance) {
        value =  chance;
    }
		
	public override void updateGettingDamage(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 - value) / 100));
    }
}
