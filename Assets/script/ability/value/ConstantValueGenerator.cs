using UnityEngine;
using System.Collections;

public class ConstantValueGenerator : AbstractValueGenerator {

	public int value;

    public ConstantValueGenerator(int value) {
        this.value = value;
    }

	public override int getValue() {
        return value;
    }
}
