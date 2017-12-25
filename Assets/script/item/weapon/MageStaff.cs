using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MageStaff : Item {
	public MageStaff(Person person) : base(person) {
        init();
    }

	public override void init() {
        cost = Constants.ITEM_MAGE_STAFF_COST;
        maxDurability = Constants.ITEM_MAGE_STAFF_DURABILITY;
        name = Constants.ITEM_MAGE_STAFF_NAME;
        level = Constants.ITEM_MAGE_STAFF_LEVEL;

        Ability attack = new MeleeAttack(owner, "Melee Attack by "  + Constants.ITEM_MAGE_STAFF_NAME);
        attack.timeCast = Constants.ITEM_MAGE_STUFF_ATTACK_SPEED;
        attack.effectList[0].valueGenerator = new ConstantValueGenerator(Constants.ITEM_MAGE_STAFF_DAMAGE);
        attack.abilityTactic = new MeleeAttackTactic(Constants.ITEM_MAGE_STAFF_LEVEL);

        attack.effectList.Add(getUseItem());

        abilityList.Add(attack);

        modificatorList.Add(new CritChanceModificator(100));

        image = Resources.Load<Sprite>("texture/Items/Weapons/stuff");
        type = ItemType.WEAPON;

        base.init();
    }
}
