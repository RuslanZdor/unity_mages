using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Xml;
using UnityEngine.UI;
using System.IO;

public class LoadGameController : GameScene, IListenerObject {

    void Start() {
        registerListener(this);

        foreach (Transform load in gameObject.transform.Find("table")) {
            if (File.Exists(load.transform.GetComponent<LoadGameField>().fileName)) {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(load.transform.GetComponent<LoadGameField>().fileName);
                load.transform.Find("header").GetComponent<Text>().text = xmldoc["savedGame"]["userName"].InnerText;
                load.transform.Find("time").GetComponent<Text>().text = xmldoc["savedGame"]["time"].InnerText;
            }else {
                load.transform.Find("header").GetComponent<Text>().text = "New Game";
                load.transform.Find("time").GetComponent<Text>().text = "00:00:00";
            }
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_LOAD_GAME) {
            enable();
        }
        if (message.type == MessageType.CLOSE_LOAD_GAME) {
            disable();
        }
    }
}
