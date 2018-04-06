public class AddDamageModificator : AbstractModificator {

    public AbstractAbilityEffect effect;

    public AddDamageModificator(AbstractAbilityEffect effect) {
        this.effect = effect;
        this.effect.attribures.Add(EffectAttribures.AUTO_GENERATED);
    }

    public override void updateMakingDamage(Ability ability) {
        if (!ability.hasAttribute(EffectAttribures.AUTO_GENERATED)) {
            var e = new BasicDamageEvent();
            var ab = new Ability();
            ab.name = "addition damage";
            ab.setAbstractTactic(new MeleeAttackTactic());

            var dae = (AbstractAbilityEffect) effect.Clone();
            dae.value = effect.valueGenerator.getValue();
            ab.effectList.Add(dae);
            ab.animationTime = 0.0f;
            e.owner = owner;
            e.target = target;
            e.ability = ab;
            e.eventTime = 0.0f;
            EventQueueSingleton.queue.add(e);
        }
    }

    public override void updateLevel(int level) {
        effect.updateLevel(level);
    }
}