using UnityEngine;
using System.Collections;

public class DamageAbility : Ability {

    public float minDamage;
    public float minDamagePerLevel;

    public float maxDamage;
    public float maxDamagePerLevel;
    
    public DamageAbility(AbstractTactic tactic) : base(tactic) {
        abilityTactic = tactic;
    }

    public override void initAbility() {
        AbstractAbilityEffect effect = new DamageAbilityEffect();
        effect.targetsNumber = 1;
        effect.valueGenerator = new RangeValueGenerator(minDamage + (level -1) * minDamagePerLevel, maxDamage + (level - 1) * maxDamagePerLevel);
        effect.attribures.Add(EffectAttribures.MELEE_ATTACK);

        effectList.Clear();
        effectList.Add(effect);
    }
}