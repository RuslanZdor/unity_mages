using UnityEngine;
using System.Collections;

public class TrollMace : Item{
	public TrollMace(Person person) : base(person) {
        init();
    }

    public override void init() {
        cost = Constants.ITEM_TROLL_MACE_COST;
        maxDurability = Constants.ITEM_TROLL_MACE_DURABILITY;
        name = Constants.ITEM_TROLL_MACE_NAME;
        level = Constants.ITEM_TROLL_MACE_LEVEL;

        Ability attack = new MeleeAttack(owner, "Melee Attack by "  + Constants.ITEM_TROLL_MACE_NAME);
        attack.timeCast = Constants.ITEM_TROLL_MACE_ATTACK_SPEED;
        attack.effectList[0].valueGenerator = new ConstantValueGenerator(Constants.ITEM_TROLL_MACE_DAMAGE);
        attack.abilityTactic = new MeleeAttackTactic(Constants.ITEM_TROLL_MACE_LEVEL);

        attack.effectList.Add(getUseItem());

        abilityList.Add(attack);
        image = Resources.Load<Sprite>("texture/Items/Weapons/mace");
        type = ItemType.WEAPON;

        base.init();
    }
}
