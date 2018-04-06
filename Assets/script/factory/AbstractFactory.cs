using script;
using UnityEngine;

public interface AbstractFactory {

    // Use this for initialization
    GameObject create(Person person, string name);
    GameObject create(Person person);
}
