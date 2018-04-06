using script;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButtonController : MonoBehaviour, IPointerClickHandler {

    public bool isActive;

    public void OnPointerClick(PointerEventData eventData) {
        if (isActive) {
            var gm = new GameMessage(MessageType.SELL_SHOP_ITEM);
            GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);
        }
    }

    public void makeActive() {
        isActive = true;
        transform.Find(Constants.BACKGROUND).GetComponent<Image>().color =
        new Color((float)24 / 256, (float)252 / 256, (float)39 / 256, (float)157 / 256);
    }

    public void makeNonActive() {
        isActive = false;
        transform.Find(Constants.BACKGROUND).GetComponent<Image>().color =
        new Color((float)252 / 256, (float)252 / 256, (float)252 / 256, (float)157 / 256);
    }
}
