using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLFactory {

    public static Item loadItem(string name) {
        Object[] assetList = Resources.LoadAll(name);
        int randomIndex = Random.Range(0, assetList.Length);
        TextAsset textAsset = (TextAsset)assetList[randomIndex];

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode xmlItem = xmldoc.GetElementsByTagName("item").Item(0);

        Item item = new Item();
        item.cost = System.Int32.Parse(xmlItem["cost"].InnerText);
        item.maxDurability = System.Int32.Parse(xmlItem["durability"].InnerText);
        item.name = xmlItem["name"].InnerText;
        item.level = System.Int32.Parse(xmlItem["level"].InnerText);
        item.image = Constants.loadSprite(xmlItem["sprite"].InnerText, xmlItem["image"].InnerText);
        item.type = getItemType(xmlItem["type"].InnerText);
        item.powerCost = System.Int32.Parse(xmlItem["powerCost"].InnerText);
        item.powerCostPerLevel = System.Int32.Parse(xmlItem["powerCostPerLevel"].InnerText);

        item.modificatorList.AddRange(getModificators(xmlItem["buffs"]));

        if (ItemType.WEAPON == item.type) {
            DamageAbility attack = new MeleeAttack("Melee Attack by " + item.name);
            attack.abilityTactic.defaultPriority = 2;
            attack.level = item.level;
            attack.timeCast = float.Parse(xmlItem["cast_time"].InnerText);
            attack.minDamage = float.Parse(xmlItem["damage"].InnerText);
            attack.minDamagePerLevel = float.Parse(xmlItem["damagePerLevel"].InnerText);
            attack.maxDamage = float.Parse(xmlItem["damage"].InnerText);
            attack.maxDamagePerLevel = float.Parse(xmlItem["damagePerLevel"].InnerText);
            item.abilityList.Add(attack);
        }

        return item;
    }

    public static List<Item> getItems(XmlNode items) {
        List<Item> list = new List<Item>();
        foreach (XmlNode item in items) {
            list.Add(loadItem(item.InnerText));
        }
        return list;
    }

    public static List<Buff> getModificators(XmlNode modificators) {
        List<Buff> list = new List<Buff>();
        foreach (XmlNode modificator in modificators) {
            if ("PassiveCritChance".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveCritChance(float.Parse(modificator["value"].InnerText)));
            }
            if ("PassiveBlockChance".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveBlockChance(float.Parse(modificator["value"].InnerText)));
            }
            if ("PassiveDodgeChance".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveDodgeChance(float.Parse(modificator["value"].InnerText)));
            }
            if ("PassiveElementalModificator".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveElementalModificator(getEffectAttribures(modificator["value"].InnerText)));
            }
            if ("PassiveMeleeDamage".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveMeleeDamage(float.Parse(modificator["value"].InnerText)));
            }
            if ("PassiveRegeneration".Equals(modificator["type"].InnerText)) {
                list.Add(new PassiveRegeneration(int.Parse(modificator["value"].InnerText)));
            }
        }
        return list;
    }

    private static ItemType getItemType(string name) {
        if ("WEAPON".Equals(name)) {
            return ItemType.WEAPON;
        }
        if ("SHIELD".Equals(name)) {
            return ItemType.SHIELD;
        }
        if ("ITEM".Equals(name)) {
            return ItemType.ITEM;
        }
        return ItemType.ITEM;
    }

    private static EffectAttribures getEffectAttribures(string name) {
        if ("WATER".Equals(name)) {
            return EffectAttribures.WATER;
        }
        if ("FIRE".Equals(name)) {
            return EffectAttribures.FIRE;
        }
        if ("AIR".Equals(name)) {
            return EffectAttribures.AIR;
        }
        if ("EARTH".Equals(name)) {
            return EffectAttribures.EARTH;
        }
        if ("COLD".Equals(name)) {
            return EffectAttribures.COLD;
        }
        if ("ELECTRICITY".Equals(name)) {
            return EffectAttribures.ELECTRICITY;
        }
        if ("POISON".Equals(name)) {
            return EffectAttribures.POISON;
        }
        if ("DARK".Equals(name)) {
            return EffectAttribures.DARK;
        }
        if ("LIGHT".Equals(name)) {
            return EffectAttribures.LIGHT;
        }
        if ("PHYSICS".Equals(name)) {
            return EffectAttribures.PHYSICS;
        }
        return EffectAttribures.PHYSICS;
    }

    public static Person loadPerson(string name) {
        TextAsset textAsset = (TextAsset)Resources.Load(name);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode xmlPerson = xmldoc.GetElementsByTagName("person").Item(0);

        Person person = new Person();
        person.name = xmlPerson["name"].InnerText;
        person.level = System.Int32.Parse(xmlPerson["level"].InnerText);
        person.experience = System.Int32.Parse(xmlPerson["experience"].InnerText);
        person.powerCost = System.Int32.Parse(xmlPerson["powerCost"].InnerText);
        person.powerCostPerLevel = System.Int32.Parse(xmlPerson["powerCostPerLevel"].InnerText);
        person.basicHealth = System.Int32.Parse(xmlPerson["maxHealth"].InnerText);
        person.healthPerLevel = System.Int32.Parse(xmlPerson["healthPerLevel"].InnerText);
        person.manaPerLevel = System.Int32.Parse(xmlPerson["manaPerLevel"].InnerText);
        person.basicMana = System.Int32.Parse(xmlPerson["maxMana"].InnerText);
        person.numberParrallelCasts = System.Int32.Parse(xmlPerson["numberParrallelCasts"].InnerText);
        person.personImage = xmlPerson["personImage"].InnerText;
        person.personModel = xmlPerson["personModel"].InnerText;

        person.itemList.AddRange(getItems(xmlPerson["items"]));
        person.buffList.AddRange(getModificators(xmlPerson["buffs"]));
        person.knownAbilities.AddRange(getAbilities(xmlPerson["abilities"]));

        return person;
    }

    public static List<Ability> getAbilities(XmlNode modificators) {
        List<Ability> list = new List<Ability>();
        foreach (XmlNode modificator in modificators) {
            if ("SummonGolem".Equals(modificator.InnerText)) {
                list.Add(new SummonGolem(new SummonCastTactic(3)));
            }
        }
        return list;
    }
}