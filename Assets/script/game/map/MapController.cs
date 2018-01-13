using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MapController : GameScene, IListenerObject {

    public GameObject fightPoint;

    public FightStartController fight;

    private List<MapPoint> fights = new List<MapPoint>();
    private MapPoint currentMapPoint;

    void Start() {
        registerListener(this);
        gameObject.SetActive(false);
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
        fights.Clear();
        loadMapFromFile();
        currentMapPoint = null;

        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;

        printFightPoints();
    }

    public void closeFightMap() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void openFightScene() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_MAP));

        GameMessage gm2 = new GameMessage(MessageType.OPEN_FIGHT_SCENE);
        gm2.parameters.Add(currentMapPoint);
        generateMessage(gm2);
    }

    private void printFightPoints() {
        GameObject map = transform.Find("Map").gameObject;

        foreach (Transform child in map.transform) {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < fights.Count; i++) {
            GameObject mapPoint = Instantiate(fightPoint, map.transform, false);
            mapPoint.GetComponent<MapPointController>().mapPoint = fights[i];
            mapPoint.transform.localPosition = new Vector2(-4.5f + (fights[i].position.x * 2.2f), -2.0f + (fights[i].position.y * 2.2f));
            mapPoint.transform.Find("Name").GetComponent<Text>().text = "power " + fights[i].fightPower.ToString();

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
        currentMapPoint = mp;
        openFightScene();
    }

    public void loadMapFromFile() {
        TextAsset textAsset = (TextAsset)Resources.Load("configs/maps/first");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        XmlNode map = xmldoc.GetElementsByTagName("map").Item(0);
        foreach (XmlNode xmlMapPoint in map) {
            MapPoint mapPoint = new MapPoint();
            mapPoint.id = System.Int32.Parse(xmlMapPoint["id"].InnerText);
            mapPoint.fightPower = System.Int32.Parse(xmlMapPoint["fightPower"].InnerText);
            if (xmlMapPoint["final"] != null 
                && (xmlMapPoint["final"].InnerText == "true")) {
                mapPoint.isFinal = true;
            }else {
                mapPoint.isFinal = false;
            }
            if (xmlMapPoint["dependList"].InnerText.Length > 0) {
                mapPoint.dependList.AddRange(xmlMapPoint["dependList"].InnerText.Split(','));
            }

            mapPoint.position = new Vector2();
            mapPoint.position.x = System.Int32.Parse(xmlMapPoint["position"]["x"].InnerText);
            mapPoint.position.y = System.Int32.Parse(xmlMapPoint["position"]["y"].InnerText);

            if (mapPoint.dependList.Count == 0) {
                mapPoint.isEnable = true;
            }
            fights.Add(mapPoint);
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_FIGHT_MAP) {
            gameObject.SetActive(true);
            if (currentMapPoint != null && currentMapPoint.isFinal) {
                closeFightMap();
            }
        }
        if (message.type == MessageType.INIT_FIGHT_MAP) {
            init();
        }

        if (message.type == MessageType.CLOSE_FIGHT_MAP) {
            gameObject.SetActive(false);
        }
        if (message.type == MessageType.FIGHT_FINISH_HERO_WINS) {
            foreach (MapPoint mp in fights) {
                if (mp.dependList.Contains(currentMapPoint.id.ToString())) {
                    mp.isEnable = true;
                }
            }
            printFightPoints();
        }
    }
}
