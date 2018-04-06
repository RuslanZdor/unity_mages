using script;

public abstract class AbstractTactic {

	public int defaultPriority;
	public Person person;
	public Ability ability;

    public AbstractTactic() {
    }

    public AbstractTactic(int defaultPriority) {
        this.defaultPriority = defaultPriority;
    }
		
    abstract public int getPriority();
}
