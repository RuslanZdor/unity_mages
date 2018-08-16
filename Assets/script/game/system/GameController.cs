using System.Collections.Generic;
using script.system.xml;
using UnityEngine;

namespace script.game {
    public class GameController : MonoBehaviour, IListenerObject {
    
        public GameScene mainMenu;
        public GameScene inventory;
        public GameScene fightMap;
        public GameScene fightScene;
        public GameScene fightResult;
        public GameScene dialog;
        public GameScene skills;
        public GameScene positions;
        public GameScene loadGame;
        public GameScene missionResult;
        public GameScene shop;
        public GameScene startNewGame;

        // Use this for initialization
        public void Start() {
            Instantiate(mainMenu);
            Instantiate(inventory);
            Instantiate(fightMap);
            Instantiate(fightScene);
            Instantiate(fightResult);
            Instantiate(dialog);
            Instantiate(skills);
            Instantiate(positions);
            Instantiate(loadGame);
            Instantiate(missionResult);
            Instantiate(shop);
            Instantiate(startNewGame);

            GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>()
                .addMessage(new GameMessage(MessageType.OPEN_LOAD_GAME));

            GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(this);
        }


        public void readMessage(GameMessage message) {
            switch (message.type) {
                case MessageType.LOAD_SAVED_GAME:
                    PartiesSingleton.currentGame = XMLGame.getInstance().loadSavedGame((string) message.parameters[0]);
                    GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().generateShop();
                    break;
                case MessageType.START_NEW_GAME:
                    PartiesSingleton.currentGame = XMLGame.getInstance().createNewGame();
                    PartiesSingleton.currentGame.saveLink = (string) message.parameters[1];
                    PartiesSingleton.currentGame.userName = (string) message.parameters[0];

                    GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().openMainMenu();
                    GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().generateShop();
                    break;
                case MessageType.SAVE_GAME:
                    XMLGame.getInstance().saveGame(PartiesSingleton.currentGame);
                    break;
                case MessageType.GENERATE_SHOP:
                    PartiesSingleton.currentShop = new Shop {
                        gold = 100,
                        items = new List<Item> {
                            XMLFactory.loadItem("configs/items/weapons/mage_stuff"),
                            XMLFactory.loadItem("configs/items/shields/shield")
                        }
                    };
                    break;
            }
        }
    }
}
