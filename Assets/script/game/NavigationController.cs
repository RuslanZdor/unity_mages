using UnityEngine;

namespace script.game
{
    public class NavigationController : MonoBehaviour {
        private void generateMessage(GameMessage gm) {
            GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addMessage(gm);
        }

        public void closeActiveWindow() {
            generateMessage(new GameMessage(MessageType.CLOSE_ACTIVE_WINDOW));
        }

        public void openInventory() {
            generateMessage(new GameMessage(MessageType.OPEN_INVENTORY));
        }

        public void openSkills() {
            generateMessage(new GameMessage(MessageType.OPEN_SKILLS));
        }

        public void openPositions() {
            generateMessage(new GameMessage(MessageType.OPEN_POSITIONS));
        }

        public void openShop() {
            generateMessage(new GameMessage(MessageType.OPEN_SHOP));
        }

        public void openMainMenu() {
            generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
        }

        public void openMissionResult() {
            generateMessage(new GameMessage(MessageType.OPEN_MISSION_RESULT));
        }

        public void openFightScene(MapPoint mapPoint) {
            var gm2 = new GameMessage(MessageType.OPEN_FIGHT_SCENE);
            gm2.parameters.Add(mapPoint);
            generateMessage(gm2);
        }

        public void saveGame() {
            generateMessage(new GameMessage(MessageType.SAVE_GAME));
        }

        public void generateShop() {
            generateMessage(new GameMessage(MessageType.GENERATE_SHOP));
        }

        public void openFightMap() {
            generateMessage(new GameMessage(MessageType.OPEN_FIGHT_MAP));
        }

        public void openFightResult() {
            generateMessage(new GameMessage(MessageType.OPEN_FIGHT_RESULT));
        }
    }
}
