using UnityEngine;
using System.Collections;

public class PassiveBlockChance : Buff {
	public PassiveBlockChance(Person person, float value) : base(person, new DamageSpellCastTactic(3)) {
        name = "Passive Block Change";

        modificator = new BlockChanceModificator(value);
    }
}