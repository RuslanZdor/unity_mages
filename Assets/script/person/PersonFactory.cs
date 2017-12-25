using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonFactory : MonoBehaviour, AbstractFactory{

    private GameObject controller;

    public static int currentID = 0;
    public GameObject basicPerson;
    public GameObject buffIcon;

    public List<Person> availableEnemy = new List<Person>();

    public List<Person> generatePersonList(int powerCost) {
        List<Person> list = new List<Person>();

        int count = Random.Range(2, 6);
        
        int generated = 0;
        for (int i = 0; i < count; i++) {
            Person p = generatePersonByPower((powerCost - generated) / (count - i));
            if (p != null) {
                generated += p.calculatePower();
                list.Add(p);
            }
        }

        while(generated < powerCost) {
            foreach (Person p in list) {
                p.level++;

                generated = 0;
                foreach (Person p2 in list) {
                    generated += p2.calculatePower();
                }

                if (generated >= powerCost) {
                    break;
                }
            }
        }

        return list;
    }

    public Person generatePersonByPower(int powerCost) {
        List<Person> canBeCreated = new List<Person>();
        canBeCreated.AddRange(availableEnemy.FindAll((Person p) => p.powerCost <= powerCost));
        if (canBeCreated.Count > 0) {
            int r = Random.Range(0, canBeCreated.Count);
            return canBeCreated[r];
        }
        return null;
    }

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

    public void setController(GameObject controller) {
        this.controller = controller;
    }

    public static int getNextId() {
        return currentID++;
    }
}

