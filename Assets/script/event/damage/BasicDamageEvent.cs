public class BasicDamageEvent : BasicTargetEvent {

    public BasicDamageEvent() {
        eventDuration = 0.5f;
    } 

    public override float eventStart() {

		foreach (var buff in owner.effectList) {
            if (buff.modificator != null) {
                buff.modificator.owner = owner;
                buff.modificator.target = target;
                buff.modificator.updateMakingDamage(ability);
            }
        }

		foreach (var buff in target.effectList) {
            if (buff.modificator != null) {
                buff.modificator.owner = target;
                buff.modificator.target = owner;
                buff.modificator.updateGettingDamage(ability);
            }
        }

        float value = target.damage(ability);
        owner.statistics.damageDealed += value;
        owner.updateAgro(owner.agro + 1);
        target.updateAgro(target.agro - 1);

        logEvent(" deal " + value + " to " + target.name + "[" + target.health + "/" + target.maxHealth + "]");
 
        if (value > 0) {
            if (ability.hasAttribute(EffectAttribures.MAGIC_SHIELD)) {
                target.personController.magicShieldTrigger();
            }else if (ability.hasAttribute(EffectAttribures.BLOCK)) {
                target.personController.blockTrigger();
            } else {
                target.personController.hittenTrigger();
            }
            return eventDuration;
        }

        if (!target.isAlive) {
            logEvent(" kill " + target.name);
            CSVLogger.log(eventTime, owner.name, GetType().ToString(), owner.name + " kill " + target.name);
        }

        return 0.0f;
    }

}
