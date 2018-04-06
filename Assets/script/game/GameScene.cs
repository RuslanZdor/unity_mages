using script;
using script.game;
using UnityEngine;

public class GameScene : MonoBehaviour{

    public bool isFinished;
    public bool isHided;
    public bool needUpdate;

    public string background;

    public void generateMessage(GameMessage gm) {
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);
    }

    public void registerListener(IListenerObject listener) {
        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(listener);
    }

    public NavigationController navigation() {
        return GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>();
    }

    public void enable() {
        gameObject.SetActive(true);
    }

    public void disable() {
        gameObject.SetActive(false);
    }
}