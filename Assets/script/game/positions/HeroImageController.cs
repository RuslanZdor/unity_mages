using script;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroImageController : PersonBehavior, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        var gm = new GameMessage(MessageType.SELECT_HERO);
        gm.parameters.Add(person);
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);
        transform.root.GetComponent<CanReload>().reload();
    }

}
