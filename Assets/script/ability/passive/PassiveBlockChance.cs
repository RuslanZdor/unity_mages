using UnityEngine;
using System.Collections;

public class PassiveBlockChance : Buff {
	public PassiveBlockChance(Person person, double value) : base(person, new DamageSpellCastTactic(3)) {
        name = "Passive Block Change";

        modificator = new BlockChanceModificator(value);
    }
}