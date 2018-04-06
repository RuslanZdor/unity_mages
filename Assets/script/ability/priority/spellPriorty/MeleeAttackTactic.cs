public class MeleeAttackTactic : AbstractTactic {

    public MeleeAttackTactic() {
    }

	public MeleeAttackTactic(int defaultPriority) : base(defaultPriority) {
    }

	public override int getPriority() {
        return defaultPriority;
    }
}
