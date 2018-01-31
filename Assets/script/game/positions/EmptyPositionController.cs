using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EmptyPositionController : PersonBehavior, IPointerClickHandler {

    public Vector2 place;
    public bool heroStatus = false;

    public void OnPointerClick(PointerEventData eventData) {
        GameMessage gm = new GameMessage(MessageType.SELECT_PLACE);
        gm.parameters.Add(place);
        gm.parameters.Add(heroStatus);
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);

        gm = new GameMessage(MessageType.SELECT_HERO);
        gm.parameters.Add(null);
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);

        transform.root.GetComponent<CanReload>().reload();
    }

}
