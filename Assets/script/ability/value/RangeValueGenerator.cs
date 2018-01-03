using UnityEngine;
using System.Collections;

public class RangeValueGenerator : AbstractValueGenerator {

	public float minValue;
	public float maxValue;

	public override float getValue() {
		return  Random.Range(minValue, maxValue);
    }

    public RangeValueGenerator(float minValue, float maxValue) {
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

}
