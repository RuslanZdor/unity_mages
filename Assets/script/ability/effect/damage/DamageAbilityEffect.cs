using System.Collections.Generic;
using script;

public class DamageAbilityEffect : AbstractAbilityEffect {

	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {
        foreach(var p in getTargets(target, ab)) {
            apply(owner, p, startTime, ab);
        }
    }

    public void apply (Person owner, Person target, float startTime, Ability ab) {
        var e = new BasicDamageEvent();

        value = valueGenerator.getValue();
        var ability = new Ability();
        ability.setAbstractTactic(new MeleeAttackTactic());
        ability.effectList.Add((AbstractAbilityEffect)Clone());

        ability.animation = ab.animation;

        e.owner = owner;
        e.target = target;
        e.ability = ability;
        e.eventTime = startTime;

        EventQueueSingleton.queue.add(e);
    }

    public override void updateLevel(int level) {
        valueGenerator.updateLevel(level);
    }

    public List<Person> getTargets(Person target, Ability ab) {
        if (ab.hasAttribute(EffectAttribures.ROW_DAMAGE)) {
            return PartiesSingleton.getParty(target.ally).getLivePersons().
            FindAll(p => p.place.x == target.place.x);
        }
        if (ab.hasAttribute(EffectAttribures.PIRCING_DAMAGE)) {
            return PartiesSingleton.getParty(target.ally).getLivePersons().
                        FindAll(p => p.place.y == target.place.y);
         }

        var list = new List<Person>();
        list.Add(target);
        return list;
    }
}
