using System;
using System.Collections;
using System.Collections.Generic;

public class AbstractTargetTactic {

    public virtual List<Person> getTargets(Party party, int count, Ability ability) {
        return null;
    }

    public void shuffle(List<Person> list) {
        int n = list.Count;
        Random rnd = new Random();
        while (n > 1) {
            int k = (rnd.Next(0, n) % n);
            n--;
            Person value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public override string ToString() {
        return base.ToString();
    }

    public bool isMelee(Ability ability) {
        return ability.effectList.FindAll(
                (AbstractAbilityEffect eff) =>
                eff.attribures.FindAll(
                    (EffectAttribures attr) => attr == EffectAttribures.MELEE_ATTACK
                ).Count > 0
            ).Count > 0;
    }
}
