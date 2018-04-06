public class IncreaseMakingShieldModificator : AbstractModificator{

    public float value;

    public IncreaseMakingShieldModificator(float chance) {
        value =  chance;
    }
		
	public override void updateMakingShield(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
