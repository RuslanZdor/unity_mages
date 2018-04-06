using script;
using UnityEngine;
using UnityEngine.UI;

public class HeroListController : PersonBehavior, IListenerObject {

    public GameObject heroImage;

    public void Start() {
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(this);
    }

    public void reload() {
        foreach (Transform child in transform) {
            if (child.name.Contains("Hero")) {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < PartiesSingleton.selectedHeroes.Count; i++) {
            var p = PartiesSingleton.selectedHeroes[i];
            var himage = Instantiate(heroImage, transform, false);
            himage.transform.localPosition = new Vector2(0.0f, 2.2f - 2.2f * i);
            himage.transform.Find("HeroImage").gameObject.GetComponent<HeroImageController>().person = p;

            if (p.Equals(person)) {
                himage.transform.Find(Constants.BACKGROUND).GetComponent<Image>().color = 
                    new Color((float)24 / 256, (float)252 / 256, (float)39 / 256, (float)157 / 256);
            }else {
                himage.transform.Find(Constants.BACKGROUND).GetComponent<Image>().color = 
                    new Color((float)148 / 256, (float)252 / 256, (float)155 / 256, (float)157 / 256);
            }
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.SELECT_HERO
            && message.parameters.Count > 0) {
            person = (Person)message.parameters[0];
        }
    }
}
