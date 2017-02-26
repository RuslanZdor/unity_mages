using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementalMiultiplicators {
	public static Dictionary<EffectAttribures, Dictionary<EffectAttribures, double>> multiplicators = new Dictionary<EffectAttribures, Dictionary<EffectAttribures, double>>();
    
	static ElementalMiultiplicators() {
		Dictionary<EffectAttribures, double> waterMultiplicators = new Dictionary<EffectAttribures, double>();
		waterMultiplicators.Add(EffectAttribures.WATER, 1.0);
        waterMultiplicators.Add(EffectAttribures.FIRE, 2.0);
        waterMultiplicators.Add(EffectAttribures.AIR, 0.5);
        waterMultiplicators.Add(EffectAttribures.EARTH, 2.0);
        waterMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5);
        waterMultiplicators.Add(EffectAttribures.COLD, 0.5);
        waterMultiplicators.Add(EffectAttribures.POISON, 2.0);
        waterMultiplicators.Add(EffectAttribures.DARK, 1.0);
        waterMultiplicators.Add(EffectAttribures.LIGHT, 1.0);
        waterMultiplicators.Add(EffectAttribures.PHYSICS, 1.0);
        multiplicators.Add(EffectAttribures.WATER, waterMultiplicators);

        Dictionary<EffectAttribures, double> fireMultiplicators = new Dictionary<EffectAttribures, double>();
        fireMultiplicators.Add(EffectAttribures.WATER, 0.5);
        fireMultiplicators.Add(EffectAttribures.FIRE, 1.0);
        fireMultiplicators.Add(EffectAttribures.AIR, 0.5);
        fireMultiplicators.Add(EffectAttribures.EARTH, 1.0);
        fireMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0);
        fireMultiplicators.Add(EffectAttribures.COLD, 2.0);
        fireMultiplicators.Add(EffectAttribures.POISON, 0.5);
        fireMultiplicators.Add(EffectAttribures.DARK, 2.0);
        fireMultiplicators.Add(EffectAttribures.LIGHT, 1.0);
        fireMultiplicators.Add(EffectAttribures.PHYSICS, 2.0);
        multiplicators.Add(EffectAttribures.FIRE, fireMultiplicators);

        Dictionary<EffectAttribures, double> airMultiplicators = new Dictionary<EffectAttribures, double>();
        airMultiplicators.Add(EffectAttribures.WATER, 2.0);
        airMultiplicators.Add(EffectAttribures.FIRE, 2.0);
        airMultiplicators.Add(EffectAttribures.AIR, 1.0);
        airMultiplicators.Add(EffectAttribures.EARTH, 2.0);
        airMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5);
        airMultiplicators.Add(EffectAttribures.COLD, 0.5);
        airMultiplicators.Add(EffectAttribures.POISON, 0.5);
        airMultiplicators.Add(EffectAttribures.DARK, 1.0);
        airMultiplicators.Add(EffectAttribures.LIGHT, 1.0);
        airMultiplicators.Add(EffectAttribures.PHYSICS, 1.0);
        multiplicators.Add(EffectAttribures.AIR, airMultiplicators);

        Dictionary<EffectAttribures, double> earthMultiplicators = new Dictionary<EffectAttribures, double>();
        earthMultiplicators.Add(EffectAttribures.WATER, 0.5);
        earthMultiplicators.Add(EffectAttribures.FIRE, 1.0);
        earthMultiplicators.Add(EffectAttribures.AIR, 0.5);
        earthMultiplicators.Add(EffectAttribures.EARTH, 1.0);
        earthMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5);
        earthMultiplicators.Add(EffectAttribures.COLD, 1.0);
        earthMultiplicators.Add(EffectAttribures.POISON, 2.0);
        earthMultiplicators.Add(EffectAttribures.DARK, 1.0);
        earthMultiplicators.Add(EffectAttribures.LIGHT, 2.0);
        earthMultiplicators.Add(EffectAttribures.PHYSICS, 2.0);
        multiplicators.Add(EffectAttribures.EARTH, earthMultiplicators);

        Dictionary<EffectAttribures, double> electricityMultiplicators = new Dictionary<EffectAttribures, double>();
        electricityMultiplicators.Add(EffectAttribures.WATER, 2.0);
        electricityMultiplicators.Add(EffectAttribures.FIRE, 1.0);
        electricityMultiplicators.Add(EffectAttribures.AIR, 2.0);
        electricityMultiplicators.Add(EffectAttribures.EARTH, 1.0);
        electricityMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0);
        electricityMultiplicators.Add(EffectAttribures.COLD, 0.5);
        electricityMultiplicators.Add(EffectAttribures.POISON, 0.5);
        electricityMultiplicators.Add(EffectAttribures.DARK, 2.0);
        electricityMultiplicators.Add(EffectAttribures.LIGHT, 1.0);
        electricityMultiplicators.Add(EffectAttribures.PHYSICS, 0.5);
        multiplicators.Add(EffectAttribures.ELECTRICITY, electricityMultiplicators);

        Dictionary<EffectAttribures, double> coldMultiplicators = new Dictionary<EffectAttribures, double>();
        coldMultiplicators.Add(EffectAttribures.WATER, 2.0);
        coldMultiplicators.Add(EffectAttribures.FIRE, 0.5);
        coldMultiplicators.Add(EffectAttribures.AIR, 2.0);
        coldMultiplicators.Add(EffectAttribures.EARTH, 1.0);
        coldMultiplicators.Add(EffectAttribures.ELECTRICITY, 2.0);
        coldMultiplicators.Add(EffectAttribures.COLD, 1.0);
        coldMultiplicators.Add(EffectAttribures.POISON, 1.0);
        coldMultiplicators.Add(EffectAttribures.DARK, 1.0);
        coldMultiplicators.Add(EffectAttribures.LIGHT, 0.5);
        coldMultiplicators.Add(EffectAttribures.PHYSICS, 0.5);
        multiplicators.Add(EffectAttribures.COLD, coldMultiplicators);

        Dictionary<EffectAttribures, double> poisonMultiplicators = new Dictionary<EffectAttribures, double>();
        poisonMultiplicators.Add(EffectAttribures.WATER, 0.5);
        poisonMultiplicators.Add(EffectAttribures.FIRE, 2.0);
        poisonMultiplicators.Add(EffectAttribures.AIR, 2.0);
        poisonMultiplicators.Add(EffectAttribures.EARTH, 0.5);
        poisonMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0);
        poisonMultiplicators.Add(EffectAttribures.COLD, 1.0);
        poisonMultiplicators.Add(EffectAttribures.POISON, 1.0);
        poisonMultiplicators.Add(EffectAttribures.DARK, 1.0);
        poisonMultiplicators.Add(EffectAttribures.LIGHT, 0.5);
        poisonMultiplicators.Add(EffectAttribures.PHYSICS, 2.0);
        multiplicators.Add(EffectAttribures.POISON, poisonMultiplicators);

        Dictionary<EffectAttribures, double> darkMultiplicators = new Dictionary<EffectAttribures, double>();
        darkMultiplicators.Add(EffectAttribures.WATER, 1.0);
        darkMultiplicators.Add(EffectAttribures.FIRE, 0.5);
        darkMultiplicators.Add(EffectAttribures.AIR, 1.0);
        darkMultiplicators.Add(EffectAttribures.EARTH, 1.0);
        darkMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5);
        darkMultiplicators.Add(EffectAttribures.COLD, 1.0);
        darkMultiplicators.Add(EffectAttribures.POISON, 2.0);
        darkMultiplicators.Add(EffectAttribures.DARK, 1.0);
        darkMultiplicators.Add(EffectAttribures.LIGHT, 2.0);
        darkMultiplicators.Add(EffectAttribures.PHYSICS, 2.0);
        multiplicators.Add(EffectAttribures.DARK, darkMultiplicators);

        Dictionary<EffectAttribures, double> lightMultiplicators = new Dictionary<EffectAttribures, double>();
        lightMultiplicators.Add(EffectAttribures.WATER, 1.0);
        lightMultiplicators.Add(EffectAttribures.FIRE, 0.5);
        lightMultiplicators.Add(EffectAttribures.AIR, 1.0);
        lightMultiplicators.Add(EffectAttribures.EARTH, 2.0);
        lightMultiplicators.Add(EffectAttribures.ELECTRICITY, 1.0);
        lightMultiplicators.Add(EffectAttribures.COLD, 2.0);
        lightMultiplicators.Add(EffectAttribures.POISON, 2.0);
        lightMultiplicators.Add(EffectAttribures.DARK, 2.0);
        lightMultiplicators.Add(EffectAttribures.LIGHT, 1.0);
        lightMultiplicators.Add(EffectAttribures.PHYSICS, 1.0);
        multiplicators.Add(EffectAttribures.LIGHT, lightMultiplicators);

        Dictionary<EffectAttribures, double> physicsMultiplicators = new Dictionary<EffectAttribures, double>();
        physicsMultiplicators.Add(EffectAttribures.WATER, 1.0);
        physicsMultiplicators.Add(EffectAttribures.FIRE, 0.5);
        physicsMultiplicators.Add(EffectAttribures.AIR, 1.0);
        physicsMultiplicators.Add(EffectAttribures.EARTH, 2.0);
        physicsMultiplicators.Add(EffectAttribures.ELECTRICITY, 0.5);
        physicsMultiplicators.Add(EffectAttribures.COLD, 2.0);
        physicsMultiplicators.Add(EffectAttribures.POISON, 1.0);
        physicsMultiplicators.Add(EffectAttribures.DARK, 0.5);
        physicsMultiplicators.Add(EffectAttribures.LIGHT, 0.5);
        physicsMultiplicators.Add(EffectAttribures.PHYSICS, 1.0);
        multiplicators.Add(EffectAttribures.PHYSICS, physicsMultiplicators);
    }

    public static double getMultiplicator(EffectAttribures first, EffectAttribures second) {
		if (multiplicators.ContainsKey(first)
			&& multiplicators[first].ContainsKey(second)) {
			return multiplicators[first][second];
        } else {
            return 1.0;
        }
    }
}