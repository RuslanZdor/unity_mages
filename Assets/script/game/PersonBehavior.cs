using UnityEngine;
using System.Collections;

public abstract class PersonBehavior : MonoBehaviour, HasPerson {
    public Person person;

    public virtual void setPerson(Person person) {
        this.person = person;
    }
}
