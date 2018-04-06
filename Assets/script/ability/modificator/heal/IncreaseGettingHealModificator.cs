public class IncreaseGettingHealModificator : AbstractModificator{

    public float value;

    public IncreaseGettingHealModificator(float chance) {
        value =  chance;
    }
		
	public override void updateGettingHeal(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
