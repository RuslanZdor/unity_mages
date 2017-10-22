using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private GameScene currentScene;

    public GameScene fightController;
    public GameScene dialogController;

    List<GameScene> sceneList = new List<GameScene>();


    // Use this for initialization
    void Start() {
        sceneList.Add(dialogController);
        sceneList.Add(fightController);

        createNextScene();
    }

    // Update is called once per frame
    void Update() {
        if (currentScene.isFinished) {
            destroyScene();
            createNextScene();
        }
    }

    private void createNextScene() {
        if (sceneList.ToArray().Length > 0) {
            currentScene = Instantiate(sceneList.ToArray()[0]);
            sceneList.RemoveAt(0);
        }
    }

    private void destroyScene() {
        Destroy(currentScene.gameObject);
    }
}
