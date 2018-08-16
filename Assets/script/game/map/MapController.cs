using script;
using UnityEngine;
using UnityEngine.UI;

public class MapController : GameScene, IListenerObject {

    public GameObject fightPoint;
    private MissionMap missionMap;

    void Start() {
        registerListener(this);
        disable();
    }

    void Update() {
        base.Update();
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.F)) {
                closeFightMap();
            }
        }

        if (isActive && needUpdate) {
            printFightPoints();
            needUpdate = false;
        }
    }

    private void init() {
        missionMap = MapFactory.loadMissionFromTemplate("configs/maps/templates/smallRoadToBoss", 1000);
        missionMap.currentMapPoint = null;

        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;

        printFightPoints();

        PartiesSingleton.currentGame.activeHeroes.Clear();
        PartiesSingleton.currentGame.activeHeroes.AddRange(PartiesSingleton.currentGame.selectedHeroes);
        foreach (var p in PartiesSingleton.currentGame.activeHeroes) {
            p.initHealthMana();
        }
    }

    public void closeFightMap() {
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    public void openMissionResult() {
        navigation().closeActiveWindow();
        navigation().openMissionResult();
    }

    public void openFightScene() {
        navigation().closeActiveWindow();
        navigation().openFightScene(missionMap.currentMapPoint);
    }

    private void printFightPoints() {
        var map = transform.Find("Map").gameObject;

        foreach (Transform child in map.transform) {
            if (child.name.Contains("Point")) {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < missionMap.fights.Count; i++) {
            var mapPoint = Instantiate(fightPoint, map.transform, false);
            mapPoint.GetComponent<MapPointController>().mapPoint = missionMap.fights[i];
            mapPoint.transform.localPosition = new Vector2(-4.5f + missionMap.fights[i].position.x * 2.2f, -2.0f + missionMap.fights[i].position.y * 2.2f);
            mapPoint.transform.Find("Name").GetComponent<Text>().text = "power " + missionMap.fights[i].fightPower;

            if (mapPoint.GetComponent<MapPointController>().mapPoint.isEnable) {
                mapPoint.GetComponent<MapPointController>().enablePoint();
            } else {
                mapPoint.GetComponent<MapPointController>().disablePoint();
            }
        }
    }

    public void openFight(MapPoint mp) {
        needUpdate = true;
        missionMap.currentMapPoint = mp;
        openFightScene();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_FIGHT_MAP) {
            enable();
            if (missionMap.currentMapPoint != null && missionMap.currentMapPoint.isFinal) {
                openMissionResult();
            }
        }
        if (message.type == MessageType.INIT_FIGHT_MAP) {
            init();
        }

        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
        if (message.type == MessageType.FIGHT_FINISH_HERO_WINS) {
            foreach (var mp in missionMap.fights) {
                if (mp.dependList.Contains(missionMap.currentMapPoint.id.ToString())) {
                    mp.isEnable = true;
                }
            }
            printFightPoints();
        }
    }
}
