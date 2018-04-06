using System.Collections.Generic;

public class ElementalMiultiplicators {
	public static Dictionary<EffectAttribures, Dictionary<EffectAttribures, float>> multiplicators = new Dictionary<EffectAttribures, Dictionary<EffectAttribures, float>>();
    
	static ElementalMiultiplicators() {
		var waterMultiplicators = new Dictionary<EffectAttribures, float>();
		waterMultiplicators.Add(EffectAttribures.WATER, 1.0f);
        waterMultiplicators.Add(EffectAttribures.FIRE, 2.0f);
        waterMultiplicators.Add(EffectAttribures.AIR, 0.5f);
        waterMultiplicators.Add(EffectAttribures.EARTH, 2.0f);
        waterMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5f);
        waterMultiplicators.Add(EffectAttribures.COLD, 0.5f);
        waterMultiplicators.Add(EffectAttribures.POISON, 2.0f);
        waterMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        waterMultiplicators.Add(EffectAttribures.LIGHT, 1.0f);
        waterMultiplicators.Add(EffectAttribures.PHYSICS, 1.0f);
        multiplicators.Add(EffectAttribures.WATER, waterMultiplicators);

        var fireMultiplicators = new Dictionary<EffectAttribures, float>();
        fireMultiplicators.Add(EffectAttribures.WATER, 0.5f);
        fireMultiplicators.Add(EffectAttribures.FIRE, 1.0f);
        fireMultiplicators.Add(EffectAttribures.AIR, 0.5f);
        fireMultiplicators.Add(EffectAttribures.EARTH, 1.0f);
        fireMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0f);
        fireMultiplicators.Add(EffectAttribures.COLD, 2.0f);
        fireMultiplicators.Add(EffectAttribures.POISON, 0.5f);
        fireMultiplicators.Add(EffectAttribures.DARK, 2.0f);
        fireMultiplicators.Add(EffectAttribures.LIGHT, 1.0f);
        fireMultiplicators.Add(EffectAttribures.PHYSICS, 2.0f);
        multiplicators.Add(EffectAttribures.FIRE, fireMultiplicators);

        var airMultiplicators = new Dictionary<EffectAttribures, float>();
        airMultiplicators.Add(EffectAttribures.WATER, 2.0f);
        airMultiplicators.Add(EffectAttribures.FIRE, 2.0f);
        airMultiplicators.Add(EffectAttribures.AIR, 1.0f);
        airMultiplicators.Add(EffectAttribures.EARTH, 2.0f);
        airMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5f);
        airMultiplicators.Add(EffectAttribures.COLD, 0.5f);
        airMultiplicators.Add(EffectAttribures.POISON, 0.5f);
        airMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        airMultiplicators.Add(EffectAttribures.LIGHT, 1.0f);
        airMultiplicators.Add(EffectAttribures.PHYSICS, 1.0f);
        multiplicators.Add(EffectAttribures.AIR, airMultiplicators);

        var earthMultiplicators = new Dictionary<EffectAttribures, float>();
        earthMultiplicators.Add(EffectAttribures.WATER, 0.5f);
        earthMultiplicators.Add(EffectAttribures.FIRE, 1.0f);
        earthMultiplicators.Add(EffectAttribures.AIR, 0.5f);
        earthMultiplicators.Add(EffectAttribures.EARTH, 1.0f);
        earthMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5f);
        earthMultiplicators.Add(EffectAttribures.COLD, 1.0f);
        earthMultiplicators.Add(EffectAttribures.POISON, 2.0f);
        earthMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        earthMultiplicators.Add(EffectAttribures.LIGHT, 2.0f);
        earthMultiplicators.Add(EffectAttribures.PHYSICS, 2.0f);
        multiplicators.Add(EffectAttribures.EARTH, earthMultiplicators);

        var electricityMultiplicators = new Dictionary<EffectAttribures, float>();
        electricityMultiplicators.Add(EffectAttribures.WATER, 2.0f);
        electricityMultiplicators.Add(EffectAttribures.FIRE, 1.0f);
        electricityMultiplicators.Add(EffectAttribures.AIR, 2.0f);
        electricityMultiplicators.Add(EffectAttribures.EARTH, 1.0f);
        electricityMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0f);
        electricityMultiplicators.Add(EffectAttribures.COLD, 0.5f);
        electricityMultiplicators.Add(EffectAttribures.POISON, 0.5f);
        electricityMultiplicators.Add(EffectAttribures.DARK, 2.0f);
        electricityMultiplicators.Add(EffectAttribures.LIGHT, 1.0f);
        electricityMultiplicators.Add(EffectAttribures.PHYSICS, 0.5f);
        multiplicators.Add(EffectAttribures.ELECTRICITY, electricityMultiplicators);

        var coldMultiplicators = new Dictionary<EffectAttribures, float>();
        coldMultiplicators.Add(EffectAttribures.WATER, 2.0f);
        coldMultiplicators.Add(EffectAttribures.FIRE, 0.5f);
        coldMultiplicators.Add(EffectAttribures.AIR, 2.0f);
        coldMultiplicators.Add(EffectAttribures.EARTH, 1.0f);
        coldMultiplicators.Add(EffectAttribures.ELECTRICITY, 2.0f);
        coldMultiplicators.Add(EffectAttribures.COLD, 1.0f);
        coldMultiplicators.Add(EffectAttribures.POISON, 1.0f);
        coldMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        coldMultiplicators.Add(EffectAttribures.LIGHT, 0.5f);
        coldMultiplicators.Add(EffectAttribures.PHYSICS, 0.5f);
        multiplicators.Add(EffectAttribures.COLD, coldMultiplicators);

        var poisonMultiplicators = new Dictionary<EffectAttribures, float>();
        poisonMultiplicators.Add(EffectAttribures.WATER, 0.5f);
        poisonMultiplicators.Add(EffectAttribures.FIRE, 2.0f);
        poisonMultiplicators.Add(EffectAttribures.AIR, 2.0f);
        poisonMultiplicators.Add(EffectAttribures.EARTH, 0.5f);
        poisonMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0f);
        poisonMultiplicators.Add(EffectAttribures.COLD, 1.0f);
        poisonMultiplicators.Add(EffectAttribures.POISON, 1.0f);
        poisonMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        poisonMultiplicators.Add(EffectAttribures.LIGHT, 0.5f);
        poisonMultiplicators.Add(EffectAttribures.PHYSICS, 2.0f);
        multiplicators.Add(EffectAttribures.POISON, poisonMultiplicators);

        var darkMultiplicators = new Dictionary<EffectAttribures, float>();
        darkMultiplicators.Add(EffectAttribures.WATER, 1.0f);
        darkMultiplicators.Add(EffectAttribures.FIRE, 0.5f);
        darkMultiplicators.Add(EffectAttribures.AIR, 1.0f);
        darkMultiplicators.Add(EffectAttribures.EARTH, 1.0f);
        darkMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5f);
        darkMultiplicators.Add(EffectAttribures.COLD, 1.0f);
        darkMultiplicators.Add(EffectAttribures.POISON, 2.0f);
        darkMultiplicators.Add(EffectAttribures.DARK, 1.0f);
        darkMultiplicators.Add(EffectAttribures.LIGHT, 2.0f);
        darkMultiplicators.Add(EffectAttribures.PHYSICS, 2.0f);
        multiplicators.Add(EffectAttribures.DARK, darkMultiplicators);

        var lightMultiplicators = new Dictionary<EffectAttribures, float>();
        lightMultiplicators.Add(EffectAttribures.WATER, 1.0f);
        lightMultiplicators.Add(EffectAttribures.FIRE, 0.5f);
        lightMultiplicators.Add(EffectAttribures.AIR, 1.0f);
        lightMultiplicators.Add(EffectAttribures.EARTH, 2.0f);
        lightMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0f);
        lightMultiplicators.Add(EffectAttribures.COLD, 2.0f);
        lightMultiplicators.Add(EffectAttribures.POISON, 2.0f);
        lightMultiplicators.Add(EffectAttribures.DARK, 2.0f);
        lightMultiplicators.Add(EffectAttribures.LIGHT, 1.0f);
        lightMultiplicators.Add(EffectAttribures.PHYSICS, 1.0f);
        multiplicators.Add(EffectAttribures.LIGHT, lightMultiplicators);

        var physicsMultiplicators = new Dictionary<EffectAttribures, float>();
        physicsMultiplicators.Add(EffectAttribures.WATER, 1.0f);
        physicsMultiplicators.Add(EffectAttribures.FIRE, 0.5f);
        physicsMultiplicators.Add(EffectAttribures.AIR, 1.0f);
        physicsMultiplicators.Add(EffectAttribures.EARTH, 2.0f);
        physicsMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5f);
        physicsMultiplicators.Add(EffectAttribures.COLD, 2.0f);
        physicsMultiplicators.Add(EffectAttribures.POISON, 1.0f);
        physicsMultiplicators.Add(EffectAttribures.DARK, 0.5f);
        physicsMultiplicators.Add(EffectAttribures.LIGHT, 0.5f);
        physicsMultiplicators.Add(EffectAttribures.PHYSICS, 1.0f);
        multiplicators.Add(EffectAttribures.PHYSICS, physicsMultiplicators);
    }

    public static float getMultiplicator(EffectAttribures first, EffectAttribures second)
    {
	    if (multiplicators.ContainsKey(first)
			&& multiplicators[first].ContainsKey(second)) {
			return multiplicators[first][second];
        }

	    return 1.0f;
    }
}