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

        item.modificatorList.AddRange(getModificators(xmlItem["modificators"]));

        if (ItemType.WEAPON == item.type) {
            Ability attack = new Ability();
            attack.setAbstractTactic(new DamageSpellCastTactic(2));
            attack.name = "Melee Attack by " + item.name;
            attack.abilityTactic.defaultPriority = 2;
            attack.level = item.level;
            attack.timeCast = float.Parse(xmlItem["cast_time"].InnerText);
            attack.targetTactic = new RandomTargetTactic();
            attack.targetType = AbilityTargetType.ENEMY;

            DamageAbilityEffect effect = new DamageAbilityEffect();
            effect.valueGenerator = new ConstantValueGenerator(float.Parse(xmlItem["damage"].InnerText));
            effect.attribures.Add(EffectAttribures.MELEE_ATTACK);
            effect.targetsNumber = 1;

            attack.effectList.Add(effect);
            item.abilityList.Add(attack);
        }

        return item;
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
        person.basicMana = System.Int32.Parse(xmlPerson["maxMana"].InnerText);
        person.numberParrallelCasts = System.Int32.Parse(xmlPerson["numberParrallelCasts"].InnerText);
        person.personImage = xmlPerson["personImage"].InnerText;
        person.personModel = xmlPerson["personModel"].InnerText;

        foreach (XmlNode item in xmlPerson["items"]) {
            person.itemList.Add(loadItem(item.InnerText));
        }

        foreach(XmlNode node in xmlPerson["abilities"]) {
            person.knownAbilities.Add(loadAbility(node.InnerText));
        }

        if (xmlPerson["skillSet"] != null) {
            person.knownAbilities.AddRange(loadSkillSet(xmlPerson["skillSet"].InnerText));
        }

        return person;
    }

    public static Ability loadAbility(string abilityLink) {
        TextAsset textAsset = (TextAsset)Resources.Load(abilityLink);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode xmlAbility = xmldoc.GetElementsByTagName("ability").Item(0);

        Ability ability = null;
        if ("basicAbility".Equals(xmlAbility["type"].InnerText)) {
            ability = new Ability();
            ability.setAbstractTactic(new DamageSpellCastTactic(3));
        }
        if ("summonAbility".Equals(xmlAbility["type"].InnerText)) {
            Person summon = loadPerson(xmlAbility["summon"].InnerText);
            SummonAbility summonAbility = new SummonAbility();

            SummonCastTactic tactic = new SummonCastTactic(3);
            tactic.summon = summon;

            summonAbility.setAbstractTactic(tactic);
            summonAbility.person = summon;
            ability = summonAbility;
        }
        if ("buffAbility".Equals(xmlAbility["type"].InnerText)
            || "passiveAbility".Equals(xmlAbility["type"].InnerText)) {
            Buff buff = new Buff();
            buff.setAbstractTactic(new DamageSpellCastTactic(3));
            buff.modificator = getModificators(xmlAbility["modificators"])[0];
            buff.duration = float.Parse(xmlAbility["duration"].InnerText);
            ability = buff;
        }
        if ("activeBuff".Equals(xmlAbility["type"].InnerText)) {
            ability = new ActiveBuff();
            ability.setAbstractTactic(new DamageSpellCastTactic(3));
        }

        ability.name = xmlAbility["name"].InnerText;
        ability.type = xmlAbility["type"].InnerText;
        ability.timeCast = float.Parse(xmlAbility["timeCast"].InnerText);
        ability.manaCost = float.Parse(xmlAbility["manaCost"].InnerText);
        ability.cooldown = float.Parse(xmlAbility["cooldown"].InnerText);
        ability.targetType = getTargetType(xmlAbility["targetType"].InnerText);
        ability.targetTactic = getTargetTactic(xmlAbility["targetTactic"].InnerText, ability);
        ability.animation = xmlAbility["animation"].InnerText;
        ability.image = Constants.loadSprite(xmlAbility["sprite"].InnerText, xmlAbility["image"].InnerText);
        ability.effectList.AddRange(getEffects(xmlAbility["effects"], ability));

        return ability;
    }

    public static List<AbstractAbilityEffect> getEffects(XmlNode xmlEffects, object obj) {
        List<AbstractAbilityEffect> effects = new List<AbstractAbilityEffect>();
        foreach (XmlNode xmlEffect in xmlEffects) {
            AbstractAbilityEffect effect = null;
            if ("RowDamageAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
                effect = new RowDamageAbilityEffect();
            }
            if ("DamageAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
                effect = new DamageAbilityEffect();
            }
            if ("AddBuffEffect".Equals(xmlEffect["type"].InnerText)) {
                AddBuffEffect buffEffect = new AddBuffEffect();
                buffEffect.buff = (Buff) obj;
                effect = buffEffect;
            }
            if ("HealAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
                effect = new HealAbilityEffect();
            }
            if ("SummonEffect".Equals(xmlEffect["type"].InnerText)) {
                SummonEffect sumEffect = new SummonEffect();
                sumEffect.person = ((SummonAbility)obj).person;
                effect = sumEffect;
            }
            if ("UseItemEffect".Equals(xmlEffect["type"].InnerText)) {
                effect = new UseItemEffect();
            }

            effect.targetsNumber = int.Parse(xmlEffect["targetsNumber"].InnerText);

            foreach (XmlNode attribute in xmlEffect["attributes"]) {
                effect.attribures.Add(getEffectAttribures(attribute.InnerText));
            }

            effect.valueGenerator = getValueGenerator(xmlEffect["valueGenerator"]);
            effects.Add(effect);
        }
        return effects;
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
        AbilityTargetType att = AbilityTargetType.FRIEND;
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
        List<AbstractModificator> list = new List<AbstractModificator>();
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
        }
        return list;
    }

    public static List<Ability> loadSkillSet(string link) {
        List<Ability> result = new List<Ability>();
        TextAsset textAsset = (TextAsset)Resources.Load(link);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode xmlAbility = xmldoc.GetElementsByTagName("skillSet").Item(0);

        foreach (XmlNode skill in xmlAbility) {
            Ability ability = loadAbility(skill["ability"].InnerText);
            ability.setRequiredLevel(int.Parse(skill["requiredLevel"].InnerText));
            ability.position = new Vector2();
            ability.position.x = System.Int32.Parse(skill["position"]["x"].InnerText);
            ability.position.y = System.Int32.Parse(skill["position"]["y"].InnerText);
            ability.isActive = true;

            result.Add(ability);
        }

        return result;
    }
}