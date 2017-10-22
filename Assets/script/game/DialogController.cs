using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogController : GameScene {

    public GameObject message;
    private GameObject currentMessage;

    List<DialogMessage> messages = new List<DialogMessage>();

    void Start() {
        DialogMessage m = new DialogMessage();
        m.message = "same message";
        m.position = DialogMessagePosition.LEFT;
        messages.Add(m);

        m = new DialogMessage();
        m.message = "another message";
        m.position = DialogMessagePosition.RIGHT;
        messages.Add(m);

        createNextMessage();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isFinished) {
            destroyMessage();
            createNextMessage();
        }
    }

    private void createNextMessage() {
        if (messages.ToArray().Length > 0) {
            currentMessage = Instantiate(message);
            currentMessage.transform.Find("MessageBlock").GetComponent<Text>().text = messages.ToArray()[0].message;
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
        currentMessage.transform.position = new Vector2(-5.0f, 0.5f);
    }

    private void moveMessageToRight() {
        currentMessage.transform.position = new Vector2(5.0f, 0.5f);

        Vector3 theScale = currentMessage.transform.Find("PersonImage").transform.localScale;
        theScale.x *= -1;
        currentMessage.transform.Find("PersonImage").transform.localScale = theScale;
        currentMessage.transform.Find("PersonImage").GetComponent<RectTransform>().localPosition = new Vector2(5.5f, 4.5f);
    }
}
