using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnFromMapController : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        GameMessage gm = new GameMessage();
        gm.type = MessageType.CLOSE_FIGHT_MAP;
        gm.message = "close fight map";
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);

        GameMessage gm2 = new GameMessage();
        gm2.type = MessageType.OPEN_MAIN_MENU;
        gm2.message = "open main menu";
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm2);
    }
}
