using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item {
	public List<AbstractModificator> modificatorList = new List<AbstractModificator>();
	public List<Ability> abilityList = new List<Ability>();
	public double cost;
	public int maxDurability;
	public int durability;
	public Person owner;
	public int level;
    public string name;

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
