using UnityEngine;
using UnityEngine.UI;

public class StartNewGameController : GameScene, IListenerObject {

    private string link;

    void Start() {
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        registerListener(this);
        isFinished = true;
        disable();
    }

    void Update() {
    }

    public void readMessage(GameMessage message) {
       if (message.type == MessageType.OPEN_START_NEW_GAME) {
            enable();
            link = (string) message.parameters[0];
        }
        if (message.type == MessageType.CLOSE_START_NEW_GAME) {
            disable();
        }
    }

    public void startNewGame() {
        Person person = XMLFactory.loadPerson("configs/monsters/heroes/mage");
        PartiesSingleton.selectedHeroes.Add(person);

        MessageController mc = GameObject.Find("MessageController").GetComponent<MessageController>();
        GameMessage gm = new GameMessage(MessageType.CLOSE_START_NEW_GAME);
        mc.addMessage(gm);

        gm = new GameMessage(MessageType.START_NEW_GAME);
        gm.parameters.Add(transform.Find("NameInput/InputField/Text").GetComponent<Text>().text);
        gm.parameters.Add(link);
        mc.addMessage(gm);
    }
}
