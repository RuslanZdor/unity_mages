using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HeroImageController : PersonBehavior, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        GameMessage gm = new GameMessage(MessageType.SELECT_HERO);
        gm.parameters.Add(person);
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);
        transform.root.GetComponent<CanReload>().reload();
    }

}
