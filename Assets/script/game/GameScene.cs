using UnityEngine;

public class GameScene : MonoBehaviour{

    public bool isFinished;
    public bool isHided;
    public bool needUpdate;

    public string background;

    public bool isFinishedOrHidded() {
        return isFinished || isHided;
    }

    public void generateMessage(GameMessage gm) {
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);
    }
    public void registerListener(IListenerObject listener) {
        GameObject.Find("MessageController").GetComponent<MessageController>().addListener(listener);
    }
}