using script;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptyPositionController : PersonBehavior, IPointerClickHandler {

    public Vector2 place;
    public bool heroStatus;

    public void OnPointerClick(PointerEventData eventData) {
        var gm = new GameMessage(MessageType.SELECT_PLACE);
        gm.parameters.Add(place);
        gm.parameters.Add(heroStatus);
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);

        gm = new GameMessage(MessageType.SELECT_HERO);
        gm.parameters.Add(null);
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);

        transform.root.GetComponent<CanReload>().reload();
    }

}
