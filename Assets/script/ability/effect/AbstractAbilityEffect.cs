using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class AbstractAbilityEffect : ICloneable{

    public AbstractValueGenerator valueGenerator;
	public float value;
	public int targetsNumber;

	public List<EffectAttribures> attribures = new List<EffectAttribures>();

	public object Clone() {
        AbstractAbilityEffect ae = (AbstractAbilityEffect) this.MemberwiseClone();
        ae.attribures = new List<EffectAttribures>();
        ae.attribures.AddRange(attribures);
        return ae;
	}

    public abstract void applyEffect(Person owner, Person target, float startTime, Ability ability);

    public abstract void updateLevel(int level);

}
