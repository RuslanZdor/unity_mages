using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class Item {
	public List<AbstractModificator> modificatorList = new List<AbstractModificator>();
	public List<Ability> abilityList = new List<Ability>();
	public float cost;
	public int maxDurability;
	public int durability;
	public Person owner;
	public int level;
    public string name;
    public string description;
    public Sprite image;
    public ItemType type;

    public AbstractAbilityEffect getUseItem() {
        UseItemEffect useItem = new UseItemEffect();
		useItem.item = this;
        return useItem;
    }

    public Item(Person person) {
		owner = person;
    }

	public virtual void init() {
        durability = maxDurability;
    }
}
