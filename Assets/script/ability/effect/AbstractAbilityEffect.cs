using System;
using System.Collections.Generic;
using script;

public abstract class AbstractAbilityEffect : ICloneable{

    public AbstractValueGenerator valueGenerator;
	public float value;
	public int targetsNumber;

	public List<EffectAttribures> attribures = new List<EffectAttribures>();

	public object Clone() {
        var ae = (AbstractAbilityEffect) MemberwiseClone();
        ae.attribures = new List<EffectAttribures>();
        ae.attribures.AddRange(attribures);
        return ae;
	}

    public abstract void applyEffect(Person owner, Person target, float startTime, Ability ability);

    public abstract void updateLevel(int level);

}
