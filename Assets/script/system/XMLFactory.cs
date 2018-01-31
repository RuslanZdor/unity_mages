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

        foreach (XmlNode node in xmlItem["abilities"]) {
            item.abilityList.Add(loadAbility(node.InnerText));
        }
        foreach (XmlNode node in xmlItem["userAbilities"]) {
            item.userAbilityList.Add(loadAbility(node.InnerText));
        }

        if (ItemType.WEAPON == item.type) {
            Ability attack = new Ability();
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
        if ("WEAPON".Equals(name)) {
            return ItemType.WEAPON;
        }
        if ("SHIELD".Equals(name)) {
            return ItemType.SHIELD;
        }
        if ("ACTIVE_ITEM".Equals(name)) {
            return ItemType.ACTIVE_ITEM;
        }
        return ItemType.ACTIVE_ITEM;
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
        if ("MELEE_ATTACK".Equals(name)) {
            return EffectAttribures.MELEE_ATTACK;
        }
        if ("ROW_DAMAGE".Equals(name)) {
            return EffectAttribures.ROW_DAMAGE;
        }
        if ("PIRCING_DAMAGE".Equals(name)) {
            return EffectAttribures.PIRCING_DAMAGE;
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
        foreach (XmlNode node in xmlAbility["effects"]) {
            ability.effectList.Add(getEffect(node, ability));
        }

        return ability;
    }

    public static AbstractAbilityEffect getEffect(XmlNode xmlEffect, object obj) {
        AbstractAbilityEffect effect = null;
        if ("DamageAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
            effect = new DamageAbilityEffect();
        }
        if ("AddBuffEffect".Equals(xmlEffect["type"].InnerText)) {
            AddBuffEffect buffEffect = new AddBuffEffect();
            buffEffect.buff = (Buff) obj;
            effect = buffEffect;
        }
        if ("RowAddBuffEffect".Equals(xmlEffect["type"].InnerText)) {
            AddBuffEffect buffEffect = new RowAddBuffEffect();
            buffEffect.buff = (Buff)obj;
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
        if ("AddShieldAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
            effect = new AddShieldAbilityEffect();
        }
        if ("RowAddShieldAbilityEffect".Equals(xmlEffect["type"].InnerText)) {
            effect = new RowAddShieldAbilityEffect();
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
        List<Ability> result = new List<Ability>();
        TextAsset textAsset = (TextAsset)Resources.Load(link);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode xmlAbility = xmldoc.GetElementsByTagName("skillSet").Item(0);

        foreach (XmlNode skill in xmlAbility) {
            Ability ability = loadAbility(skill["ability"].InnerText);
            ability.setRequiredLevel(int.Parse(skill["requiredLevel"].InnerText));
            ability.position = new Vector2();
            ability.position.x = int.Parse(skill["position"]["x"].InnerText);
            ability.position.y = int.Parse(skill["position"]["y"].InnerText);
            ability.isActive = bool.Parse(skill["active"].InnerText);

            result.Add(ability);
        }

        return result;
    }
}