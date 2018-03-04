using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.IO;

public class LoadGameField : MonoBehaviour, IPointerClickHandler {

    public string fileName;

    public void OnPointerClick(PointerEventData eventData) {
        MessageController mc = GameObject.Find("MessageController").GetComponent<MessageController>();
        mc.addMessage(new GameMessage(MessageType.CLOSE_LOAD_GAME));

        if (File.Exists(fileName)) {
            mc.addMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));

            GameMessage gm = new GameMessage(MessageType.LOAD_SAVED_GAME);
            gm.parameters.Add(fileName);
            mc.addMessage(gm);
        } else {
            GameMessage gm = new GameMessage(MessageType.OPEN_START_NEW_GAME);
            gm.parameters.Add(fileName);
            mc.addMessage(gm);
        }
    }
}
