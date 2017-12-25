using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItemsController : MonoBehaviour, IPointerClickHandler {

	public Person person;

    public GameObject heroItem;

    public void OnPointerClick(PointerEventData eventData) {
    }

    public void reload() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < person.itemList.Count; i++) {
            GameObject item = Instantiate(heroItem, transform, false);
            item.transform.localPosition = new Vector2(0.0f, 2.2f - (2.2f * i));
            item.transform.GetComponent<Image>().sprite = person.itemList[i].image;
            item.GetComponent<HeroItemController>().item = person.itemList[i];
            item.GetComponent<HeroItemController>().person = person;
        }
    }
}
