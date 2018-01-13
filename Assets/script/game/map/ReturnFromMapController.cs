using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnFromMapController : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        MessageController mc = GameObject.Find("MessageController").GetComponent<MessageController>();
        mc.addMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));
        mc.addMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }
}
