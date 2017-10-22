using UnityEngine;
using System.Collections;

public interface AbstractFactory {

    // Use this for initialization
    GameObject create<T>(string name) where T : Person;
    GameObject create(Person person);
}
