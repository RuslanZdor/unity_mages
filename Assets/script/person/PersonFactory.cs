using UnityEngine;
using System.Collections;

public class PersonFactory : MonoBehaviour, AbstractFactory{

    private int currentID = 0;
    public GameObject basicPerson;
    public GameObject buffIcon;

    public GameObject create<T>(string name) where T : Person {
        GameObject go = Instantiate(basicPerson);
        go.AddComponent<T>();
        GameObject goClone = Instantiate(go);
        Destroy(go);
        goClone.GetComponent<T>().name = name;
        goClone.GetComponent<Person>().id = currentID++;

        Sprite image = Resources.Load<Sprite>(goClone.GetComponent<Person>().personImage) as Sprite;
        goClone.transform.Find("model").GetComponent<SpriteRenderer>().sprite = image;

        return goClone;
    }

    public GameObject create(Person person) {
        GameObject go = Instantiate(basicPerson);
 //       UnityEditorInternal.ComponentUtility.CopyComponent(person);
  //      UnityEditorInternal.ComponentUtility.PasteComponentAsNew(go);
        go.AddComponent(person.GetType());
        GameObject goClone = Instantiate(go);
        Destroy(go);

        Sprite image = Resources.Load<Sprite>(goClone.GetComponent<Person>().personImage) as Sprite;
        goClone.transform.Find("model").GetComponent<SpriteRenderer>().sprite = image;
        goClone.GetComponent<Person>().id = currentID++;
        return goClone;
    }

    public GameObject createBuffIcon(Buff buff) {
        GameObject go = Instantiate(buffIcon);
        return go;

    }
}

