public class AddShieldEvent : BasicTargetEvent {
	public override float eventStart() {
        float value = target.addShield(ability);
        owner.statistics.heal += value;
        logEvent(" add shield " + value + " to "
            + target.name + "[" + target.shield + "]");
        return 0.0f;
    }

}
