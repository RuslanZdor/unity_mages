using System.Collections.Generic;
using script;
using UnityEngine;

public class BuffController : MonoBehaviour {

    private Person person;
    private readonly List<GameObject> buffs = new List<GameObject>();

    private GameObject buffList;

    // Use this for initialization
    void Start() {
        person = transform.GetComponent<PersonController>().person;
        buffList = transform.Find("model/buff").gameObject;
    }

    // Update is called once per frame
    void Update() {

        if (person.updateBuffs) {
            foreach (var child in buffs) {
                Destroy(child);
            }

            int number = 0;
            foreach (var buff in person.effectList) {
                var pf = GameObject.Find("GameFactory").GetComponent<PersonFactory>();
                var go = pf.createBuffIcon(buff);
                go.transform.SetParent(gameObject.transform.Find("model/buff").transform, false);
                go.transform.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector3(0.0f - 0.5f * (number / 4), 0.0f - 0.5f * (number % 4), 0.0f);
                number++;
                buffs.Add(go);
            }

            person.updateBuffs = false;
        }
    }
}
