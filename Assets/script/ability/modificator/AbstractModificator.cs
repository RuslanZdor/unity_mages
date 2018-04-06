using script;

public class AbstractModificator {

    public Person owner;
    public Person target;

    public virtual void updateLevel(int level) { }

	public virtual void updateMakingDamage(Ability ability) {}
	public virtual void updateGettingDamage(Ability ability) {}
    public virtual void updateMakingHeal(Ability ability) {}
    public virtual void updateGettingHeal(Ability ability) {}
    public virtual void updateMakingShield(Ability ability) {}
    public virtual void updateGettingShield(Ability ability) {}
    public virtual void updateMakingBuff(Ability ability) {}
    public virtual void updateGettingBuff(Ability ability) {}
}
