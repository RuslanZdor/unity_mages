using UnityEngine;
using System.Collections;

public class ReturnDamageModificator : AbstractModificator{

    public AbstractAbilityEffect effect;

    public ReturnDamageModificator(AbstractAbilityEffect effect) {
        this.effect = effect;
        this.effect.attribures.Add(EffectAttribures.AUTO_GENERATED);
    }


    public override void updateGettingDamage(Ability ability) {
        if (!ability.hasAttribute(EffectAttribures.AUTO_GENERATED)) {
            BasicDamageEvent e = new BasicDamageEvent();
            Ability ab = new Ability();
            ab.name = "return damage";
            ab.setAbstractTactic(new MeleeAttackTactic());

            AbstractAbilityEffect dae = (AbstractAbilityEffect)effect.Clone();
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
