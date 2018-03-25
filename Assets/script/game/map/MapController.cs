using UnityEngine;
using UnityEngine.UI;

public class MapController : GameScene, IListenerObject {

    public GameObject fightPoint;

    public FightStartController fight;

    private MissionMap missionMap;

    void Start() {
        registerListener(this);
        disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && !isFinished) {
            closeFightMap();
        }

        if (!isHided && needUpdate) {
            printFightPoints();
            needUpdate = false;
        }
    }

    private void init() {
        missionMap = MapFactory.loadMissionFromTemplate("configs/maps/templates/smallRoadToBoss", 1000);
        missionMap.currentMapPoint = null;

        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;

        printFightPoints();

        PartiesSingleton.activeHeroes.Clear();
        PartiesSingleton.activeHeroes.AddRange(PartiesSingleton.selectedHeroes);
        foreach (Person p in PartiesSingleton.activeHeroes) {
            p.initHealthMana();
        }
    }

    public void closeFightMap() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void openMissionResult() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));
        generateMessage(new GameMessage(MessageType.OPEN_MISSION_RESULT));
    }

    public void openFightScene() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));

        GameMessage gm2 = new GameMessage(MessageType.OPEN_FIGHT_SCENE);
        gm2.parameters.Add(missionMap.currentMapPoint);
        generateMessage(gm2);
    }

    private void printFightPoints() {
        GameObject map = transform.Find("Map").gameObject;

        foreach (Transform child in map.transform) {
            if (child.name.Contains("Point")) {
                GameObject.Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < missionMap.fights.Count; i++) {
            GameObject mapPoint = Instantiate(fightPoint, map.transform, false);
            mapPoint.GetComponent<MapPointController>().mapPoint = missionMap.fights[i];
            mapPoint.transform.localPosition = new Vector2(-4.5f + (missionMap.fights[i].position.x * 2.2f), -2.0f + (missionMap.fights[i].position.y * 2.2f));
            mapPoint.transform.Find("Name").GetComponent<Text>().text = "power " + missionMap.fights[i].fightPower.ToString();

            if (mapPoint.GetComponent<MapPointController>().mapPoint.isEnable) {
                mapPoint.GetComponent<MapPointController>().enablePoint();
            } else {
                mapPoint.GetComponent<MapPointController>().disablePoint();
            }
        }
    }

    public void openFight(MapPoint mp) {
        isHided = true;
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

        if (message.type == MessageType.CLOSE_FIGHT_MAP) {
            disable();
        }
        if (message.type == MessageType.FIGHT_FINISH_HERO_WINS) {
            foreach (MapPoint mp in missionMap.fights) {
                if (mp.dependList.Contains(missionMap.currentMapPoint.id.ToString())) {
                    mp.isEnable = true;
                }
            }
            printFightPoints();
        }
    }
}
