using UnityEngine;
using System.Collections;

public class PersonFactory : MonoBehaviour, AbstractFactory{

    private FightStartController controller;

    public static int currentID = 0;
    public GameObject basicPerson;
    public GameObject buffIcon;

    public GameObject create(Person person, string name) {
        GameObject go = Instantiate(basicPerson);
        go.AddComponent<PersonController>();
        GameObject goClone = Instantiate(go, controller.transform, false);
        Destroy(go);

        Person clone = (Person)person.Clone();
        goClone.GetComponent<PersonController>().person = clone;
        clone.id = getNextId();
        clone.name = name;
        clone.initAbilities();

        clone.personController = goClone.GetComponent<PersonController>();

        return goClone;
    }

    public GameObject create(Person person) {
        GameObject go = Instantiate(basicPerson);
        go.AddComponent<PersonController>();
        GameObject goClone = Instantiate(go, controller.transform, false);
        Destroy(go);

        Person clone = (Person)person.Clone();
        goClone.GetComponent<PersonController>().person = clone;
        clone.id = getNextId();
        clone.initAbilities();

        clone.personController = goClone.GetComponent<PersonController>();

        return goClone;
    }

    public GameObject createBuffIcon(Buff buff) {
        GameObject go = Instantiate(buffIcon);
        return go;
    }

    public void setController(FightStartController controller) {
        this.controller = controller;
    }

    public static int getNextId() {
        return currentID++;
    }
}

