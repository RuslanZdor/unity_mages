using script;
using UnityEngine;

public abstract class PersonBehavior : MonoBehaviour, HasPerson {
    public Person person;

    public virtual void setPerson(Person person) {
        this.person = person;
    }
}
