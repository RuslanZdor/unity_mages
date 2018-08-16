using script;
using UnityEngine;
using UnityEngine.UI;

public class HeroTabController : PersonBehavior, IListenerObject {

    public GameObject heroSkill;
    public GameObject heroItem;

    public bool isItem;
    public bool isSkills;

    public void Start() {
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(this);
    }

    public void reload() {
        if (isItem) {
            reloadItems();
        }
        if (isSkills) {
            reloadSkills();
        }
    }

    public void reloadItems() {
        foreach (Transform child in transform.Find("Items")) {
            if (child.name.Contains("Hero")) {
                Destroy(child.gameObject);
            }
        }

        for (int s = 0; s < PartiesSingleton.currentGame.inventory.Count; s++) {
            var item = PartiesSingleton.currentGame.inventory[s];
            var hitem = Instantiate(heroItem, transform.Find("Items").transform, false);
            hitem.transform.localPosition = new Vector2(-3.3f + s % 4 * 2.2f, 2.2f - s / 4 * 2.2f);
            hitem.transform.GetComponent<Image>().sprite = item.image;
            hitem.GetComponent<HeroItemController>().item = item;
        }
    }

    public void reloadSkills() {
        foreach (Transform child in transform.Find("HeroSkills")) {
            if (child.name.Contains("Hero")) {
                Destroy(child.gameObject);
            }
        }

        foreach (var ab in person.knownAbilities) {
            if (ab.position.x > 0
                && ab.position.y > 0) {
                var hskill = Instantiate(heroSkill, transform.Find("HeroSkills").transform, false);
                hskill.GetComponent<SkillController>().ability = ab;
                hskill.GetComponent<SkillController>().person = person;
                hskill.transform.localPosition = new Vector2(-1.3f + (ab.position.x - 2) * 2.2f, 2.2f * (ab.position.y - 2));
                hskill.transform.GetComponent<Image>().sprite = ab.image;
                if (ab.requiredLevel > person.level) {
                    hskill.GetComponent<SkillController>().blocked();
                }else {
                    if (ab.isActive) {
                        hskill.GetComponent<SkillController>().enable();
                    } else {
                        hskill.GetComponent<SkillController>().disable();
                    }
                }
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
