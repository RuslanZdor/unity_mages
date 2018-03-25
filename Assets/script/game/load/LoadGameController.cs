using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Xml;
using UnityEngine.UI;
using System.IO;

public class LoadGameController : GameScene, IListenerObject {

    void Start() {
        registerListener(this);
        reload();
     }

    public void reload() {
        foreach (Transform load in gameObject.transform.Find("table")) {
            string link = "savedGames/" + load.name + ".xml";
            Debug.Log(link);
            if (File.Exists(link)) {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(link);
                load.transform.Find("loadField/header").GetComponent<Text>().text = xmldoc["savedGame"]["userName"].InnerText;
                load.transform.Find("loadField/time").GetComponent<Text>().text = xmldoc["savedGame"]["time"].InnerText;
            } else {
                load.transform.Find("delete").gameObject.SetActive(false);
                load.transform.Find("loadField/header").GetComponent<Text>().text = "New Game";
                load.transform.Find("loadField/time").GetComponent<Text>().text = "00:00:00";
            }
        }
    }

    public void loadGame(string link) {
        MessageController mc = GameObject.Find("MessageController").GetComponent<MessageController>();
        mc.addMessage(new GameMessage(MessageType.CLOSE_LOAD_GAME));

        if (File.Exists(link)) {
            mc.addMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));

            GameMessage gm = new GameMessage(MessageType.LOAD_SAVED_GAME);
            gm.parameters.Add(link);
            mc.addMessage(gm);
        } else {
            GameMessage gm = new GameMessage(MessageType.OPEN_START_NEW_GAME);
            gm.parameters.Add(link);
            mc.addMessage(gm);
        }
    }

    public void deleteSave(string link) {
        File.Delete(link);
        reload();
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
