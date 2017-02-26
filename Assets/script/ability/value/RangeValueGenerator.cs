using UnityEngine;
using System.Collections;

public class RangeValueGenerator : AbstractValueGenerator {

	public int minValue;
	public int maxValue;

	public override int getValue() {
		return  Random.Range(minValue, maxValue);
    }

    public RangeValueGenerator(int minValue, int maxValue) {
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

}
