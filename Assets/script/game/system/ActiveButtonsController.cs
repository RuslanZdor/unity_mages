using UnityEngine;

namespace script.game {
    public class ActiveButtonsController : GameScene {
        void Update() {
            if (Input.GetKeyDown(KeyCode.I)) {
                navigation().closeInventory();
                navigation().openInventory();
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                navigation().closeSkills();
                navigation().openSkills();
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                navigation().closeFightMap();
                navigation().openFightMap();
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                navigation().closePositions();
                navigation().openPositions();
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                navigation().closeShop();
                navigation().openShop();
            }
        }
    }
}