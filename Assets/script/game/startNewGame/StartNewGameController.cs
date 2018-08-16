using UnityEngine;
using UnityEngine.UI;

public class StartNewGameController : GameScene, IListenerObject {

    private string link;

    public void Start() {
        registerListener(this);
        isFinished = true;
        disable();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_START_NEW_GAME) {
            enable();
            link = (string) message.parameters[0];
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }

    public void startNewGame() {
        var person = XMLFactory.loadPerson("configs/monsters/heroes/mage");
        PartiesSingleton.currentGame.selectedHeroes.Add(person);

        generateMessage(new GameMessage(MessageType.CLOSE_ACTIVE_WINDOW));

        var gm = new GameMessage(MessageType.START_NEW_GAME);
        gm.parameters.Add(transform.Find("NameInput/InputField/Text").GetComponent<Text>().text);
        gm.parameters.Add(link);
        generateMessage(gm);
    }
}
