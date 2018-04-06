public class IncreaseGettingShieldModificator : AbstractModificator{

    public float value;

    public IncreaseGettingShieldModificator(float chance) {
        value =  chance;
    }
		
	public override void updateGettingShield(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
