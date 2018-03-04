using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaitingHeroesController : MonoBehaviour, IListenerObject {

	public Person person;

    public GameObject heroImage;
    public GameObject position;
    public GameObject emptyPlace;

    private bool isFinish = false;

    public void Start() {
        GameObject.Find("MessageController").GetComponent<MessageController>().addListener(this);
    }

    public void reload() {
        preparePlaces();

        foreach (Person p in PartiesSingleton.selectedHeroes.FindAll((Person p) => !p.isActive)) {
            if (p.place.x < 1 || p.place.y < 1) {
                p.place = PartiesSingleton.generatePlace();
            }
            GameObject himage = Instantiate(heroImage, transform.Find("position" + p.place.x + p.place.y), false);
            himage.transform.Find("HeroImage").gameObject.GetComponent<HeroImageController>().person = p;

            if (p.Equals(person)) {
                himage.transform.Find("Background").GetComponent<Image>().color =
                    new Color(((float)24 / 256), ((float)252 / 256), ((float)39 / 256), ((float)157 / 256));
            } else {
                himage.transform.Find("Background").GetComponent<Image>().color =
                    new Color(((float)148 / 256), ((float)252 / 256), ((float)155 / 256), ((float)157 / 256));
            }
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.SELECT_HERO
            && message.parameters.Count > 0) {
            person = (Person)message.parameters[0];
        }
        if (message.type == MessageType.SELECT_PLACE
            && message.parameters.Count > 0) {
            if (person != null) {
                person.place = (Vector2)message.parameters[0];
            }
        }
    }

    public void preparePlaces() {
        foreach (Transform child in transform) {
            foreach (Transform childPosition in child) {
                GameObject.Destroy(childPosition.gameObject);
            }
        }

        if (!isFinish) {
            for (int x = 1; x < 4; x++) {
                for (int y = 1; y < 4; y++) {
                    GameObject empty = Instantiate(position, transform, false);
                    empty.name = "position" + x + y;
                    empty.transform.localPosition = new Vector2(-4.4f + 2.2f * x, -4.4f + 2.2f * y);
                }
            }
            isFinish = true;
        }
        for (int x = 1; x < 4; x++) {
            for (int y = 1; y < 4; y++) {
                GameObject empty = Instantiate(emptyPlace, transform.Find("position" + x + y), false);
                empty.transform.Find("Image").GetComponent<EmptyPositionController>().place = new Vector2(x, y);
                empty.transform.Find("Image").GetComponent<EmptyPositionController>().heroStatus = false;
            }
        }
    }
}
