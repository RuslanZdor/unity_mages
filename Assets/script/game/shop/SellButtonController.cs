using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButtonController : MonoBehaviour, IPointerClickHandler {

    public bool isActive;

    public void OnPointerClick(PointerEventData eventData) {
        if (isActive) {
            GameMessage gm = new GameMessage(MessageType.SELL_SHOP_ITEM);
            GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);
        }
    }

    public void makeActive() {
        isActive = true;
        transform.Find("Background").GetComponent<Image>().color =
        new Color(((float)24 / 256), ((float)252 / 256), ((float)39 / 256), ((float)157 / 256));
    }

    public void makeNonActive() {
        isActive = false;
        transform.Find("Background").GetComponent<Image>().color =
        new Color(((float)252 / 256), ((float)252 / 256), ((float)252 / 256), ((float)157 / 256));
    }
}
