using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private GameScene currentScene;

    public GameScene mainMenu;

    // Use this for initialization
    void Start() {
        Person f = new FireMage();
        f.name = "Fire mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        f = new HealMage();
        f.name = "Heal mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        currentScene = changeScene(mainMenu);
    }

    // Update is called once per frame
    void Update() {
        GameScene next;
        if (currentScene.isFinishedOrHidded()) {
            next = currentScene.getNextScene();
            if (next != null) {
                currentScene.gameObject.SetActive(false);
                currentScene.isHided = true;
                next.setPreviousScene(currentScene);
            } else {
                next = currentScene.getPreviousScene();
                Destroy(currentScene.gameObject);
            }
            currentScene = changeScene(next);
        }
    }

    private GameScene changeScene(GameScene next) {
        GameScene newScene;
        if (next.gameObject.scene.name != null) {
            next.gameObject.SetActive(true);
            next.setNextScene(null);
            newScene = next;
            newScene.isHided = false;
         } else {
            newScene = Instantiate(next);
         }
        return newScene;
    }

}
