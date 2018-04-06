using script;
using UnityEngine;

public class RangeValueGenerator : AbstractValueGenerator {

	public float minValue;
	public float maxValue;

	public override float getValue() {
		return  Random.Range(minValue, maxValue);
    }

    public override void updateLevel(int level) {
        maxValue = maxValue * Constants.getMultiplayer(level) / Constants.getMultiplayer(this.level);
        minValue = minValue * Constants.getMultiplayer(level) / Constants.getMultiplayer(this.level);
        this.level = level;
    }

    public RangeValueGenerator(float minValue, float maxValue) {
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

}
