using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : GameScene, IListenerObject {

    public GameObject message;
    private GameObject currentMessage;

    readonly List<DialogMessage> messages = new List<DialogMessage>();

    void Start() {
        if (!isFinished) {
            background = "texture/main_scene";
            var image = Resources.Load<Sprite>(background);
            transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;

            registerListener(this);
            disable();

            var m = new DialogMessage();
            m.message = "same message";
            m.position = DialogMessagePosition.LEFT;
            messages.Add(m);

            m = new DialogMessage();
            m.message = "another message";
            m.position = DialogMessagePosition.RIGHT;
            messages.Add(m);

            createNextMessage();
        }
    }

    void Update() {
        base.Update();
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                destroyMessage();
                createNextMessage();
            }
        }
    }

    private void createNextMessage() {
        if (messages.ToArray().Length > 0) {
            currentMessage = Instantiate(message, transform, false);
            currentMessage.transform.Find("MessageBlock/Text").GetComponent<Text>().text = messages.ToArray()[0].message;
            if (messages.ToArray()[0].position == DialogMessagePosition.LEFT) {
                moveMessageToLeft();
            }else {
                moveMessageToRight();
            }
            messages.RemoveAt(0);
        }else {
            isFinished = true;
        }
    }

    private void destroyMessage() {
        Destroy(currentMessage.gameObject);
    }

    private void moveMessageToLeft() {
        currentMessage.transform.position = new Vector2(0.0f, -2.5f);
    }

    private void moveMessageToRight() {
        currentMessage.transform.position = new Vector2(10.0f, -2.5f);

        var theScale = currentMessage.transform.Find("PersonImage").transform.localScale;
        theScale.x *= -1;
        currentMessage.transform.Find("PersonImage").transform.localScale = theScale;

        Vector2 position = currentMessage.transform.Find("PersonImage").transform.localPosition;
        position.x = -3.0f;
        currentMessage.transform.Find("PersonImage").transform.localPosition = position;
    }

    public void readMessage(GameMessage message) {
    }
}
