using UnityEngine;
using System.Collections;

public class Constants {

    /*Person parameters*/
	public static int PERSON_BASE_HEALTH = 10;
    public static  int PERSON_BASE_MANA = 0;
    public static  int PERSON_HEALTH_PER_LEVEL = 0;
    public static  int PERSON_MANA_PER_LEVEL = 0;
    public static  int PERSON_AGRO = 10;
    public static  int PERSON_MELEE_ATTACK_SPEED = 1;
    public static  int PERSON_MELEE_ATTACK_DAMAGE = 1;

    public static  int BASE_MAGE_HEALTH = 30;
    public static  int BASE_MAGE_MANA = 10;
    public static  int BASE_MAGE_HEALTH_PER_LEVEL = 5;
    public static  int BASE_MAGE_MANA_PER_LEVEL = 1;

    public static  int TROLL_HEALTH = 100;
    public static  int TROLL_MANA = 10;
    public static  int TROLL_HEALTH_PER_LEVEL = 10;
    public static  int TROLL_MANA_PER_LEVEL = 1;

    public static  int GOLEM_HEALTH = 50;
    public static  int GOLEM_MANA = 0;
    public static  int GOLEM_HEALTH_PER_LEVEL = 10;
    public static  int GOLEM_MANA_PER_LEVEL = 0;
    public static  int GOLEM_AGRO = 50;

    /*items*/
    public static  string ITEM_MAGE_STAFF_NAME = "Mage Staff";
    public static  int ITEM_MAGE_STAFF_COST = 100;
    public static  int ITEM_MAGE_STAFF_DURABILITY = 100;
    public static  int ITEM_MAGE_STAFF_DAMAGE = 3;
    public static  int ITEM_MAGE_STUFF_ATTACK_SPEED = 1;
    public static  int ITEM_MAGE_STAFF_LEVEL = 1;

    public static  string ITEM_TROLL_MACE_NAME = "Troll Mace";
    public static  int ITEM_TROLL_MACE_COST = 100;
    public static  int ITEM_TROLL_MACE_DURABILITY = 100;
    public static  int ITEM_TROLL_MACE_DAMAGE = 5;
    public static  int ITEM_TROLL_MACE_ATTACK_SPEED = 1;
    public static  int ITEM_TROLL_MACE_LEVEL = 1;

    public static  string ITEM_SHIELD_NAME = "Shield";
    public static  int ITEM_SHIELD_COST = 100;
    public static  int ITEM_SHIELD_USABLE_COUNT = 100;
    public static  int ITEM_SHIELD_BLOCK_CHANCE = 50;
}
