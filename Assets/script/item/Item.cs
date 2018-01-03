using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item {
	public List<Buff> modificatorList = new List<Buff>();
	public List<Ability> abilityList = new List<Ability>();
	public float cost;
    public int maxDurability;
	public int durability;
	public Person owner;
    public string name;
    public string description;
    public Sprite image;
    public ItemType type;

    public int powerCost;
    public int level;
    public int powerCostPerLevel;

    public AbstractAbilityEffect getUseItem() {
        UseItemEffect useItem = new UseItemEffect();
		useItem.item = this;
        return useItem;
    }

    public Item() {
    }

	public virtual void init() {
        durability = maxDurability;
    }
}
