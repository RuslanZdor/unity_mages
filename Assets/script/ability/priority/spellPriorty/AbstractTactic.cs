using UnityEngine;
using System.Collections;

public abstract class AbstractTactic {

	public int defaultPriority = 0;
	public Person person;
	public Ability ability;

    public AbstractTactic() {
    }

    public AbstractTactic(int defaultPriority) {
        this.defaultPriority = defaultPriority;
    }
		
    abstract public int getPriority();
}
