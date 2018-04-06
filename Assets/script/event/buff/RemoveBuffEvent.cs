public class RemoveBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override float eventStart() {
        target.removeEffect(buff);
        logEvent("remove buff " + buff.name + "from " + target.name);
        return 0.0f;
    }
}
