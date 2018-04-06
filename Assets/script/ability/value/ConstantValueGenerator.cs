using script;

public class ConstantValueGenerator : AbstractValueGenerator {

	public float value;

    public ConstantValueGenerator(float value) {
        this.value = value;
    }

	public override float getValue() {
        return value;
    }

    public override void updateLevel(int level) {
        value = value * Constants.getMultiplayer(level) / Constants.getMultiplayer(this.level);
        this.level = level;
    }
}
