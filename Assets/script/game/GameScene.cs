using UnityEngine;

public class GameScene : MonoBehaviour{

    public bool isFinished;
    public bool isHided;

    public string background;

    public GameScene previousScene;
    public GameScene nextScene;

    public bool isFinishedOrHidded() {
        return isFinished || isHided;
    }

    public void setPreviousScene(GameScene scene) {
        this.previousScene = scene;
    }

    public GameScene getPreviousScene() {
        return previousScene;
    }

    public void setNextScene(GameScene scene) {
        this.nextScene = scene;
    }

    public GameScene getNextScene() {
        return nextScene;
    }

}