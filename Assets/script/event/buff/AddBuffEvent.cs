public class AddBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override float eventStart() {
        foreach (var b in owner.effectList) {
            if (b.modificator == null) continue;
            b.modificator.owner = owner;
            b.modificator.target = target;
            b.modificator.updateMakingBuff(buff);
        }

        foreach (var b in target.effectList) {
            if (b.modificator == null) continue;
            b.modificator.owner = target;
            b.modificator.target = owner;
            b.modificator.updateGettingBuff(buff);
        }

        target.addEffect(buff);
  
        if (owner.enemy.Equals(target.ally)) {
            owner.updateAgro(owner.agro + 1);
        }

        logEvent("add buff " + buff.name + " on " + target.name);

	    var removeEvent = new RemoveBuffEvent {
	        owner = owner,
	        target = target,
	        buff = buff,
	        eventTime = eventTime + buff.duration
	    };
	    EventQueueSingleton.queue.add(removeEvent);

        return 0.0f;
    }
}
