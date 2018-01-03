using UnityEngine;
using System.Collections;

public class ConstantValueGenerator : AbstractValueGenerator {

	public float value;

    public ConstantValueGenerator(float value) {
        this.value = value;
    }

	public override float getValue() {
        return value;
    }
}
