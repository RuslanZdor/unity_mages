using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : GameScene {

    private GameObject heroTab;
    private GameObject heroList;

    public GameObject heroImage;

    void Start() {
        heroTab = transform.Find("HeroTab").gameObject;
        heroList = transform.Find("HeroList").gameObject;

        for (int i = 0; i < PartiesSingleton.activeHeroes.Count; i++) {
            Person p = PartiesSingleton.activeHeroes[i];
            GameObject himage = Instantiate(heroImage, heroList.transform, false);
            himage.transform.position = new Vector2(-5.5f, 3.0f - (2.2f * i));
            himage.GetComponent<HeroImageController>().person = p;
        }

        heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().reload();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I) && !isFinished) {
            isFinished = true;
        }
    }

}
