using script;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemController : PersonBehavior, IPointerClickHandler {

	public Item item;

    public void OnPointerClick(PointerEventData eventData) {
        var gm = new GameMessage(MessageType.SELECT_SHOP_ITEM);
        gm.parameters.Add(item);
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);
    }

    public void setItem(Item currentItem) {
        transform.Find("Image").GetComponent<Image>().sprite = currentItem.image;
        item = currentItem;
    }

    public void makeActive() {
        transform.Find(Constants.BACKGROUND).GetComponent<Image>().color =
        new Color((float)24 / 256, (float)252 / 256, (float)39 / 256, (float)157 / 256);
    }
}
