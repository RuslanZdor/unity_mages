using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSideController : MonoBehaviour, IListenerObject {

    public GameObject heroItem;

    public Item item;

    public void Start() {
        GameObject.Find("MessageController").GetComponent<MessageController>().addListener(this);
    }

    public void refresh() {
        foreach (Transform child in transform.Find("Items")) {
            GameObject.Destroy(child.gameObject);
        }
        transform.Find("SellButton").GetComponent<SellButtonController>().makeNonActive();
    }

    public void sellItem() {
        if (getItemList().Contains(item)) {
            getItemList().Remove(item);
            setGold(getGold() + item.cost);
        } else {
            getItemList().Add(item);
            setGold(getGold() - item.cost);
        }
        item = null;
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.SELECT_SHOP_ITEM) {
            this.item = (Item)message.parameters[0];
            reload();
        }
        if (message.type == MessageType.SELL_SHOP_ITEM) {
            sellItem();
            reload();
        }
    }

    public abstract void reload();

    public abstract List<Item> getItemList();

    public abstract float getGold();

    public abstract void setGold(float gold);
}
