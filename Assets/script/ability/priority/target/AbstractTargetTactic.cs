using System;
using System.Collections.Generic;
using script;

public class AbstractTargetTactic {

    public virtual List<Person> getTargets(Party party, int count, Ability ability) {
        return null;
    }

    public void shuffle(List<Person> list) {
        int n = list.Count;
        var rnd = new Random();
        while (n > 1) {
            int k = rnd.Next(0, n) % n;
            n--;
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public override string ToString() {
        return base.ToString();
    }

    public bool isMelee(Ability ability) {
        return ability.effectList.FindAll(
                eff =>
                eff.attribures.FindAll(
                    attr => attr == EffectAttribures.MELEE_ATTACK
                ).Count > 0
            ).Count > 0;
    }
}
