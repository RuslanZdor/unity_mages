using UnityEngine;
using System.Collections;

public class BasicDamageEvent : BasicTargetEvent {

    public BasicDamageEvent() {
        eventDuration = 0.5f;
    } 

    public override float eventStart() {

		foreach (Buff buff in owner.effectList) {
            buff.modificator.updateMakingDamage(ability);
        }

		foreach (Buff buff in target.effectList) {
            buff.modificator.updateGettingDamage(ability);
        }

        float value = target.damage(ability);
        owner.statistics.damageDealed += value;
        owner.updateAgro(owner.agro + 1);
        target.updateAgro(target.agro - 1);

        logEvent(" deal " + value + " to " + target.name + "[" + target.health + "/" + target.maxHealth + "]");
 
        if (value > 0) {
            target.personController.hittenTrigger();
            return eventDuration;
        }

        if (!target.isAlive) {
            logEvent(" kill " + target.name);
            CSVLogger.log(eventTime, owner.name, GetType().ToString(), owner.name + " kill " + target.name);
        }

        return 0.0f;
    }

}
