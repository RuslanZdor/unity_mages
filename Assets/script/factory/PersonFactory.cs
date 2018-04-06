using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEngine.UI;

public class PersonFactory : MonoBehaviour, AbstractFactory{

    private GameObject controller;

    public static int currentID;
    public GameObject buffIcon;

    public List<Person> availableEnemy = new List<Person>();

    public List<Person> generatePersonList(MapPoint mapPoint) {
        var list = new List<Person>();

        int generated = 0;
        for (int i = 0; i < mapPoint.count; i++) {
            var p = generatePersonByPower((mapPoint.fightPower - generated) / (mapPoint.count - i));
            if (p != null) {
                generated += p.calculatePower();
                list.Add((Person) p.Clone());
            }
        }

        while(generated < mapPoint.fightPower) {
            foreach (var p in list) {
                p.setLevel(p.level + 1);

                generated = 0;
                foreach (var p2 in list) {
                    generated += p2.calculatePower();
                }

                if (generated >= mapPoint.fightPower) {
                    break;
                }
            }
        }

        return list;
    }

    public Person generatePersonByPower(int powerCost) {
        var canBeCreated = new List<Person>();
        canBeCreated.AddRange(availableEnemy.FindAll(p => p.powerCost <= powerCost));
        if (canBeCreated.Count > 0) {
            int r = Random.Range(0, canBeCreated.Count);
            return canBeCreated[r];
        }
        return null;
    }

    public GameObject create(Person person, string name) {
        var personModel = Resources.Load<GameObject>(person.personModel);
        var go = Instantiate(personModel);
        go.AddComponent<PersonController>();
        var goClone = Instantiate(go, controller.transform, false);
        Destroy(go);

        var clone = (Person)person.Clone();
        goClone.GetComponent<PersonController>().person = clone;
        clone.id = getNextId();
        clone.name = name;
        clone.initAbilities();

        clone.personController = goClone.GetComponent<PersonController>();

        return goClone;
    }

    public GameObject create(Person person) {
        var personModel = Resources.Load<GameObject>(person.personModel);
        var go = Instantiate(personModel);
        go.AddComponent<PersonController>();
        var goClone = Instantiate(go, controller.transform, false);
        Destroy(go);

        var clone = (Person)person.Clone();
        goClone.GetComponent<PersonController>().person = clone;
        clone.id = getNextId();
        clone.initAbilities();

        clone.personController = goClone.GetComponent<PersonController>();

        return goClone;
    }

    public GameObject createBuffIcon(Buff buff) {
        var go = Instantiate(buffIcon);
        go.GetComponent<Image>().sprite = buff.image;
        return go;
    }

    public void setController(GameObject controller) {
        this.controller = controller;
    }

    public static int getNextId() {
        return currentID++;
    }
}

