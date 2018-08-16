using script;
using script.game;
using UnityEngine;

public class GameScene : MonoBehaviour{

    public bool isFinished;
    public bool isActive;
    public bool needUpdate;

    public string background;

    public void Start() {
        isActive = false;
    }

    public void Update() {
        if (needUpdate) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(isActive);
            }
            needUpdate = false;
        }
    }

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
        isActive = true;
        needUpdate = true;
    }

    public void disable() {
        isActive = false;
        needUpdate = true;
    }
}