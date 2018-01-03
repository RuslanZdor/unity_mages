using UnityEngine;
using System.Collections;

public class PassiveElementalModificator : Buff {
	public PassiveElementalModificator(EffectAttribures value) : base(new DamageSpellCastTactic(3)) {
        name = "Passive Elemental Modificator";
        image = Constants.loadSprite("texture/Skills/buffIcons", "buffIcons_76");

        modificator = new ElementsDamageModificator(value);
    }
}