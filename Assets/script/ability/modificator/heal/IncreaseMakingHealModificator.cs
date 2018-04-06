public class IncreaseMakingHealModificator : AbstractModificator{

    public float value;

    public IncreaseMakingHealModificator(float chance) {
        value =  chance;
    }
		
	public override void updateMakingHeal(Ability ability) {
		ability.effectList.ForEach (effect => 
			effect.value = (int) (effect.value * (100 + value) / 100));
    }
}
