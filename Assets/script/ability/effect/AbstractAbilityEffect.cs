using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class AbstractAbilityEffect : ICloneable{

    public AbstractValueGenerator valueGenerator;
	public int value;
	public int targetsNumber;

	public List<EffectAttribures> attribures = new List<EffectAttribures>();

	public object Clone() {
		return this.MemberwiseClone();
	}

    public abstract void applyEffect(Person owner, Person target, float startTime);

}
