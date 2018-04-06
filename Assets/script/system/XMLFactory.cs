using System;
using System.Collections.Generic;
using System.Xml;
using script;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class XMLFactory {

    public static Item loadItem(string name) {
        var assetList = Resources.LoadAll(name);
        int randomIndex = Random.Range(0, assetList.Length);
        var textAsset = (TextAsset)assetList[randomIndex];

        var xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        var xmlItem = xmldoc.GetElementsByTagName("item").Item(0);

        var item = new Item {
            resource = name,
            cost = int.Parse(xmlItem["cost"].InnerText),
            maxDurability = int.Parse(xmlItem["durability"].InnerText),
            name = xmlItem["name"].InnerText,
            level = int.Parse(xmlItem["level"].InnerText),
            image = Constants.loadSprite(xmlItem["sprite"].InnerText, xmlItem["image"].InnerText),
            type = getItemType(xmlItem["type"].InnerText),
            powerCost = int.Parse(xmlItem["powerCost"].InnerText),
            powerCostPerLevel = int.Parse(xmlItem["powerCostPerLevel"].InnerText)
        };

        item.modificatorList.AddRange(getModificators(xmlItem["modificators"]));

        foreach (XmlNode node in xmlItem["abilities"]) {
            item.abilityList.Add(loadAbility(node.InnerText));
        }
        foreach (XmlNode node in xmlItem["userAbilities"]) {
            item.userAbilityList.Add(loadAbility(node.InnerText));
        }

        if (ItemType.WEAPON == item.type) {
            var attack = new Ability();
            attack.setAbstractTactic(new DamageSpellCastTactic(2));
            attack.name = "Melee Attack by " + item.name;
            attack.abilityTactic.defaultPriority = 2;
            attack.level = item.level;
            attack.timeCast = float.Parse(xmlItem["cast_time"].InnerText);
            attack.targetTactic = new RandomTargetTactic();
            attack.targetType = AbilityTargetType.ENEMY;
            attack.image = item.image;

            foreach (XmlNode node in xmlItem["effects"]) {
                attack.effectList.Add(getEffect(node, attack));
            }
            item.abilityList.Add(attack);
        }

        return item;
    }

    private static ItemType getItemType(string name) {
        switch (name) {
            case "WEAPON":
                return ItemType.WEAPON;
            case "SHIELD":
                return ItemType.SHIELD;
            case "ACTIVE_ITEM":
                return ItemType.ACTIVE_ITEM;
        }

        return ItemType.ACTIVE_ITEM;
    }

    private static EffectAttribures getEffectAttribures(string name) {
        switch (name) {
            case "WATER":
                return EffectAttribures.WATER;
            case "FIRE":
                return EffectAttribures.FIRE;
            case "AIR":
                return EffectAttribures.AIR;
            case "EARTH":
                return EffectAttribures.EARTH;
            case "COLD":
                return EffectAttribures.COLD;
            case "ELECTRICITY":
                return EffectAttribures.ELECTRICITY;
            case "POISON":
                return EffectAttribures.POISON;
            case "DARK":
                return EffectAttribures.DARK;
            case "LIGHT":
                return EffectAttribures.LIGHT;
            case "PHYSICS":
                return EffectAttribures.PHYSICS;
            case "MELEE_ATTACK":
                return EffectAttribures.MELEE_ATTACK;
            case "ROW_DAMAGE":
                return EffectAttribures.ROW_DAMAGE;
            case "PIRCING_DAMAGE":
                return EffectAttribures.PIRCING_DAMAGE;
        }

        return EffectAttribures.PHYSICS;
    }

    public static Person loadPerson(string name) {
        var textAsset = (TextAsset)Resources.Load(name);
        var xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        var xmlPerson = xmldoc.GetElementsByTagName("person").Item(0);

        var person = new Person
        {
            resource = name,
            name = xmlPerson["name"].InnerText,
            level = int.Parse(xmlPerson["level"].InnerText),
            powerCost = int.Parse(xmlPerson["powerCost"].InnerText),
            powerCostPerLevel = int.Parse(xmlPerson["powerCostPerLevel"].InnerText),
            basicHealth = int.Parse(xmlPerson["maxHealth"].InnerText),
            basicMana = int.Parse(xmlPerson["maxMana"].InnerText),
            numberParrallelCasts = int.Parse(xmlPerson["numberParrallelCasts"].InnerText),
            personImage = xmlPerson["personImage"].InnerText,
            personModel = xmlPerson["personModel"].InnerText
        };
        person.setExpirience(int.Parse(xmlPerson["experience"].InnerText));

        foreach (XmlNode item in xmlPerson["items"]) {
            person.itemList.Add(loadItem(item.InnerText));
        }

        if (xmlPerson["skillSet"] != null) {
            person.knownAbilities.AddRange(loadSkillSet(xmlPerson["skillSet"].InnerText));
        }

        return person;
    }

    public static Ability loadAbility(string abilityLink) {
        var textAsset = (TextAsset)Resources.Load(abilityLink);
        var xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        var xmlAbility = xmldoc.GetElementsByTagName("ability").Item(0);

        Ability ability = null;
        switch (xmlAbility["type"].InnerText) {
            case "basicAbility":
                ability = new Ability();
                ability.setAbstractTactic(new DamageSpellCastTactic(3));
                break;
            case "summonAbility":
                var summon = loadPerson(xmlAbility["summon"].InnerText);
                var summonAbility = new SummonAbility();

                var tactic = new SummonCastTactic(3);
                tactic.summon = summon;

                summonAbility.setAbstractTactic(tactic);
                summonAbility.person = summon;
                ability = summonAbility;
                break;
            case "buffAbility":
            case "passiveAbility":
                var buff = new Buff();
                buff.setAbstractTactic(new DamageSpellCastTactic(3));
                buff.modificator = getModificators(xmlAbility["modificators"])[0];
                buff.duration = float.Parse(xmlAbility["duration"].InnerText);
                ability = buff;
                break;
            case "activeBuff":
                ability = new ActiveBuff();
                ability.setAbstractTactic(new DamageSpellCastTactic(3));
                break;
        }

        ability.name = xmlAbility["name"].InnerText;
        ability.type = xmlAbility["type"].InnerText;
        ability.resource = abilityLink;
        ability.timeCast = float.Parse(xmlAbility["timeCast"].InnerText);
        ability.manaCost = float.Parse(xmlAbility["manaCost"].InnerText);
        ability.cooldown = float.Parse(xmlAbility["cooldown"].InnerText);
        ability.targetType = getTargetType(xmlAbility["targetType"].InnerText);
        ability.targetTactic = getTargetTactic(xmlAbility["targetTactic"].InnerText, ability);
        ability.animation = xmlAbility["animation"].InnerText;
        ability.image = Constants.loadSprite(xmlAbility["sprite"].InnerText, xmlAbility["image"].InnerText);
        foreach (XmlNode node in xmlAbility["effects"]) {
            ability.effectList.Add(getEffect(node, ability));
        }

        return ability;
    }

    public static AbstractAbilityEffect getEffect(XmlNode xmlEffect, object obj) {
        AbstractAbilityEffect effect = null;
        switch (xmlEffect["type"].InnerText) {
            case "DamageAbilityEffect":
                effect = new DamageAbilityEffect();
                break;
            case "AddBuffEffect":
            {
                var buffEffect = new AddBuffEffect {buff = (Buff) obj};
                effect = buffEffect;
                break;
            }
            case "RowAddBuffEffect":
            {
                AddBuffEffect buffEffect = new RowAddBuffEffect();
                buffEffect.buff = (Buff)obj;
                effect = buffEffect;
                break;
            }
            case "HealAbilityEffect":
                effect = new HealAbilityEffect();
                break;
            case "SummonEffect":
                var sumEffect = new SummonEffect();
                sumEffect.person = ((SummonAbility)obj).person;
                effect = sumEffect;
                break;
            case "UseItemEffect":
                effect = new UseItemEffect();
                break;
            case "AddShieldAbilityEffect":
                effect = new AddShieldAbilityEffect();
                break;
            case "RowAddShieldAbilityEffect":
                effect = new RowAddShieldAbilityEffect();
                break;
        }

        effect.targetsNumber = int.Parse(xmlEffect["targetsNumber"].InnerText);
        foreach (XmlNode attribute in xmlEffect["attributes"]) {
            effect.attribures.Add(getEffectAttribures(attribute.InnerText));
        }

        effect.valueGenerator = getValueGenerator(xmlEffect["valueGenerator"]);
        
        return effect;
    }

    public static AbstractValueGenerator getValueGenerator(XmlNode generator) {
        AbstractValueGenerator gen = null;
        if (generator != null) {
            if ("RangeValueGenerator".Equals(generator["type"].InnerText)) {
                gen = new RangeValueGenerator(float.Parse(generator["min"].InnerText), float.Parse(generator["max"].InnerText));
            }
            if ("ConstantValueGenerator".Equals(generator["type"].InnerText)) {
                gen = new ConstantValueGenerator(float.Parse(generator["value"].InnerText));
            }
        }
        return gen;
    }

    public static AbilityTargetType getTargetType(string name) {
        var att = AbilityTargetType.FRIEND;
        if ("FRIEND".Equals(name)) {
            att = AbilityTargetType.FRIEND;
        }
        if ("ENEMY".Equals(name)) {
            att = AbilityTargetType.ENEMY;
        }
        return att;
    }

    public static AbstractTargetTactic getTargetTactic(string name, object ab) {
        AbstractTargetTactic att = null;
        if ("RandomTargetTactic".Equals(name)) {
            att = new RandomTargetTactic();
        }
        if ("AllPartyTargetTactic".Equals(name)) {
            att = new AllPartyTargetTactic();
        }
        if ("DamagedTargetTactic".Equals(name)) {
            att = new DamagedTargetTactic();
        }
        if ("ItselfTargetTactic".Equals(name)) {
            att = new ItselfTargetTactic();
        }
        if ("WithoutBuffTactic".Equals(name)) {
            att = new WithoutBuffTactic((Buff) ab);
        }
        return att;
    }

    public static List<AbstractModificator> getModificators(XmlNode modificators) {
        var list = new List<AbstractModificator>();
        foreach (XmlNode buff in modificators) {
            if ("CritChanceModificator".Equals(buff["type"].InnerText)) {
                list.Add(new CritChanceModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("BlockChanceModificator".Equals(buff["type"].InnerText)) {
                list.Add(new BlockChanceModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("DodgeChanceModificator".Equals(buff["type"].InnerText)) {
                list.Add(new DodgeChanceModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("ElementsDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new ElementsDamageModificator(getEffectAttribures(buff["value"].InnerText)));
            }
            if ("IncreaseDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseDamageModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("IncreaseMeleeDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseMeleeDamageModificator(int.Parse(buff["value"].InnerText)));
            }
            if ("BuffDurationModificator".Equals(buff["type"].InnerText)) {
                list.Add(new BuffDurationModificator(int.Parse(buff["value"].InnerText)));
            }
            if ("ReturnDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new ReturnDamageModificator(getEffect(buff["effect"], null)));
            }
            if ("IncreaseMakingShieldModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseMakingShieldModificator(int.Parse(buff["value"].InnerText)));
            }
            if ("IncreaseGettingShieldModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseGettingShieldModificator(int.Parse(buff["value"].InnerText)));
            }
            if ("GettingDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new GettingDamageModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("IncreaseMakingHealModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseMakingHealModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("IncreaseGettingHealModificator".Equals(buff["type"].InnerText)) {
                list.Add(new IncreaseGettingHealModificator(float.Parse(buff["value"].InnerText)));
            }
            if ("AddDamageModificator".Equals(buff["type"].InnerText)) {
                list.Add(new AddDamageModificator(getEffect(buff["effect"], null)));
            }
        }
        return list;
    }

    public static List<Ability> loadSkillSet(string link) {
        var result = new List<Ability>();
        var textAsset = (TextAsset)Resources.Load(link);
        var xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        var xmlAbility = xmldoc.GetElementsByTagName("skillSet").Item(0);

        foreach (XmlNode skill in xmlAbility) {
            var ability = loadAbility(skill["ability"].InnerText);
            ability.setRequiredLevel(int.Parse(skill["requiredLevel"].InnerText));
            ability.position = laodPosition(skill["position"]);
            ability.isActive = bool.Parse(skill["active"].InnerText);

            result.Add(ability);
        }

        return result;
    }

    public static Vector2 laodPosition(XmlNode xmlPosition) {
        var position = new Vector2 {
            x = int.Parse(xmlPosition["x"].InnerText),
            y = int.Parse(xmlPosition["y"].InnerText)
        };
        return position;
    }
}