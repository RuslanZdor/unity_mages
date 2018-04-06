using script;
using UnityEngine;
using UnityEngine.UI;

public class HeroItemsController : PersonBehavior, IListenerObject {

    public static readonly string LEFT_HAND = "LeftHand";
    public static readonly string RIGHT_HAND = "RightHand";
    public static readonly string ARMOR = "Armor";
    public static readonly string ACTIVE_ITEM = "ActiveItem";
    
    public GameObject heroItem;

    public void Start() {
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(this);
    }

    public void reload() {
        foreach (Transform child in transform) {
            foreach (Transform i in child) {
                Destroy(i.gameObject);
            }
        }

        if (person.findItem(ItemType.WEAPON) != null) {
            var itemObject = Instantiate(heroItem, transform.Find(LEFT_HAND), false);
            itemObject.GetComponent<HeroItemController>().init(person, person.findItem(ItemType.WEAPON));
        }
        if (person.findItem(ItemType.SHIELD) != null) {
            var itemObject = Instantiate(heroItem, transform.Find(RIGHT_HAND), false);
            itemObject.GetComponent<HeroItemController>().init(person, person.findItem(ItemType.SHIELD));
        }
        if (person.findItem(ItemType.ARMOR) != null) {
            var itemObject = Instantiate(heroItem, transform.Find(ARMOR), false);
            itemObject.GetComponent<HeroItemController>().init(person, person.findItem(ItemType.ARMOR));
        }
        if (person.findItem(ItemType.ACTIVE_ITEM) != null) {
            var itemObject = Instantiate(heroItem, transform.Find(ACTIVE_ITEM), false);
            itemObject.GetComponent<HeroItemController>().init(person, person.findItem(ItemType.ACTIVE_ITEM));
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.SELECT_HERO
            && message.parameters.Count > 0) {
            person = (Person)message.parameters[0];
        }
    }
}
