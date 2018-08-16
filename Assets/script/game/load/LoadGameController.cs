using System.IO;
using System.Xml;
using script;
using script.system.xml;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameController : GameScene, IListenerObject {

    void Start() {
        base.Start();
        isActive = true;
        registerListener(this);
        reload();
     }

    public void reload() {
        foreach (Transform load in gameObject.transform.Find("table")) {
            string link = "savedGames/" + load.name + ".xml";
            Debug.Log(link);
            if (File.Exists(link)) {
                var xmldoc = new XmlDocument();
                xmldoc.Load(link);
                load.transform.Find("loadField/header").GetComponent<Text>().text = xmldoc[XMLGame.XML_SAVED_GAME][XMLGame.XML_USER_NAME].InnerText;
                load.transform.Find("loadField/time").GetComponent<Text>().text = xmldoc[XMLGame.XML_SAVED_GAME][XMLGame.XML_TIME].InnerText;
            } else {
                load.transform.Find("delete").gameObject.SetActive(false);
                load.transform.Find("loadField/header").GetComponent<Text>().text = "New Game";
                load.transform.Find("loadField/time").GetComponent<Text>().text = "00:00:00";
            }
        }
    }

    public void loadGame(string link) {
        var mc = GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>();
        navigation().closeActiveWindow();

        if (File.Exists(link)) {
            navigation().openMainMenu();
            var gm = new GameMessage(MessageType.LOAD_SAVED_GAME);
            gm.parameters.Add(link);
            mc.addMessage(gm);
        } else {
            var gm = new GameMessage(MessageType.OPEN_START_NEW_GAME);
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
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
