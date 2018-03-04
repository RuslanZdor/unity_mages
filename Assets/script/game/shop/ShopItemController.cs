using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemController : PersonBehavior, IPointerClickHandler {

	public Item item;

    public void OnPointerClick(PointerEventData eventData) {
        GameMessage gm = new GameMessage(MessageType.SELECT_SHOP_ITEM);
        gm.parameters.Add(item);
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);
    }

    public void setItem(Item currentItem) {
        transform.Find("Image").GetComponent<Image>().sprite = currentItem.image;
        this.item = currentItem;
    }

    public void makeActive() {
        transform.Find("Background").GetComponent<Image>().color =
        new Color(((float)24 / 256), ((float)252 / 256), ((float)39 / 256), ((float)157 / 256));
    }
}
