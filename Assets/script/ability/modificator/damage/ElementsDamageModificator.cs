public class ElementsDamageModificator : AbstractModificator{

    public EffectAttribures value;

    public ElementsDamageModificator(EffectAttribures value) {
        this.value =  value;
    }

	public override void updateGettingDamage(Ability ability) {
		ability.effectList.ForEach(effect => {
			foreach (var attr in effect.attribures) {
                 effect.value = (int) (effect.value * ElementalMiultiplicators.getMultiplicator(attr, value));
             }
         });
    }
}
