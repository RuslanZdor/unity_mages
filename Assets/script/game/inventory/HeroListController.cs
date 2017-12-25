using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroListController : MonoBehaviour, IPointerClickHandler {

	public Person person;

    public GameObject heroImage;

    public void OnPointerClick(PointerEventData eventData) {
    }

    public void reload() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < PartiesSingleton.activeHeroes.Count; i++) {
            Person p = PartiesSingleton.activeHeroes[i];
            GameObject himage = Instantiate(heroImage, transform, false);
            himage.transform.localPosition = new Vector2(0.0f, 2.2f - (2.2f * i));
            himage.transform.Find("HeroImage").gameObject.GetComponent<HeroImageController>().person = p;

            if (person.Equals(p)) {
                himage.transform.Find("Background").GetComponent<Image>().color = 
                    new Color(((float)24 / 256), ((float)252 / 256), ((float)39 / 256), ((float)157 / 256));
            }else {
                himage.transform.Find("Background").GetComponent<Image>().color = 
                    new Color(((float)148 / 256), ((float)252 / 256), ((float)155 / 256), ((float)157 / 256));
            }
        }
    }
}
