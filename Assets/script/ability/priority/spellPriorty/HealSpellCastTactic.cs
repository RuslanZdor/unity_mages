public class HealSpellCastTactic : AbstractTactic {
    
	public override int getPriority() {
        bool hasDamage = false;
		foreach (var p in PartiesSingleton.getParty(person.ally).getLivePersons()) {
            if (p.isDamaged()) {
                hasDamage = true;
            }
        }

        if (person.mana > ability.manaCost
                && hasDamage) {
            return defaultPriority;
        }
        return 0;
    }

    public HealSpellCastTactic(int priority) {
        defaultPriority = priority;
    }
}
