using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffController : MonoBehaviour {

    private Person person;
    private List<GameObject> buffs = new List<GameObject>();

    private GameObject buffList;

    // Use this for initialization
    void Start() {
        person = transform.GetComponent<PersonController>().person;
        buffList = transform.Find("model/buff").gameObject;
    }

    // Update is called once per frame
    void Update() {

        if (person.updateBuffs) {
            foreach (GameObject child in buffs) {
                Destroy(child);
            }

            int number = 0;
            foreach (Buff buff in person.effectList) {
                PersonFactory pf = transform.root.Find("GameFactory").GetComponent<PersonFactory>();
                GameObject go = pf.createBuffIcon(buff);
                go.transform.SetParent(gameObject.transform.Find("model/buff").transform, false);
                go.transform.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector3((0.0f - 0.5f * ((int) number / 4)), (0.0f - 0.5f * (number % 4)), 0.0f);
                number++;
                buffs.Add(go);
            }

            person.updateBuffs = false;
        }
    }
}
