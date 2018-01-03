using UnityEngine;
using System.Collections;

public class PassiveBlockChance : Buff {
	public PassiveBlockChance(float value) : base(new DamageSpellCastTactic(3)) {
        name = "Passive Block Change";
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_42");
        modificator = new BlockChanceModificator(value);
    }
}