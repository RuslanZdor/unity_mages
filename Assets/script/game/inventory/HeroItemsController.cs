using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItemsController : PersonBehavior, IListenerObject {

    public GameObject heroItem;

    public void Start() {
        GameObject.Find("MessageController").GetComponent<MessageController>().addListener(this);
    }

    public void reload() {
        foreach (Transform child in transform) {
            foreach (Transform i in child) {
                GameObject.Destroy(i.gameObject);
            }
        }

        Item item = person.findItem(ItemType.WEAPON);
        if (item != null) {
            GameObject itemObject = Instantiate(heroItem, transform.Find("LeftHand"), false);
            itemObject.GetComponent<Image>().sprite = item.image;
            itemObject.GetComponent<HeroItemController>().item = item;
            itemObject.GetComponent<HeroItemController>().person = person;
        }
        item = person.findItem(ItemType.SHIELD);
        if (item != null) {
            GameObject itemObject = Instantiate(heroItem, transform.Find("RightHand"), false);
            itemObject.GetComponent<Image>().sprite = item.image;
            itemObject.GetComponent<HeroItemController>().item = item;
            itemObject.GetComponent<HeroItemController>().person = person;
        }
        item = person.findItem(ItemType.ARMOR);
        if (item != null) {
            GameObject itemObject = Instantiate(heroItem, transform.Find("Armor"), false);
            itemObject.GetComponent<Image>().sprite = item.image;
            itemObject.GetComponent<HeroItemController>().item = item;
            itemObject.GetComponent<HeroItemController>().person = person;
        }
        item = person.findItem(ItemType.ACTIVE_ITEM);
        if (item != null) {
            GameObject itemObject = Instantiate(heroItem, transform.Find("ActiveItem"), false);
            itemObject.GetComponent<Image>().sprite = item.image;
            itemObject.GetComponent<HeroItemController>().item = item;
            itemObject.GetComponent<HeroItemController>().person = person;
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.SELECT_HERO
            && message.parameters.Count > 0) {
            person = (Person)message.parameters[0];
        }
    }
}
