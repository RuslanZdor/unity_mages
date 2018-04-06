using script;

public class AddShieldAbilityEffect : AbstractAbilityEffect {

	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {
        BasicTargetEvent e = new AddShieldEvent();

        value = valueGenerator.getValue();
        var ability = new Ability();
        ability.setAbstractTactic(new MeleeAttackTactic());
		ability.effectList.Add(this);

        ability.animation = ab.animation;

		e.ability = ability;
		e.owner = owner;
		e.target = target;
		e.eventTime = startTime;

        EventQueueSingleton.queue.add(e);
    }

    public override void updateLevel(int level) {
        valueGenerator.updateLevel(level);
    }
}
