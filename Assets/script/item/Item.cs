using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item {
	public List<AbstractModificator> modificatorList = new List<AbstractModificator>();
	public List<Ability> abilityList = new List<Ability>();
    public List<Ability> userAbilityList = new List<Ability>();
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

    public string resource;

    public AbstractAbilityEffect getUseItem() {
        UseItemEffect useItem = new UseItemEffect();
		useItem.item = this;
        return useItem;
    }

	public virtual void init() {
        durability = maxDurability;
    }

    public void setLevel(int level) {
        this.level = level;
    }
}
