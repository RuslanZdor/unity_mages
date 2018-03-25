using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MapFactory {

    public static MissionMap loadMissionFromTemplate(string link, int cost) {
        TextAsset textAsset = (TextAsset)Resources.Load(link);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNode map = xmldoc.GetElementsByTagName("map").Item(0);

        List<MapPoint> listPoints = new List<MapPoint>();
        foreach (XmlNode xmlMapPoint in map) {
            MapPoint mapPoint = new MapPoint();
            mapPoint.fightPower = int.Parse(xmlMapPoint["power"].InnerText);
            mapPoint.type = parseMapPoint(xmlMapPoint["type"].InnerText);
            listPoints.Add(mapPoint);
        }

        MissionMap missionMap = new MissionMap();
        missionMap.cost = cost;

        float summory = 0.0f;
        for (int i = 0; i < listPoints.Count; i++) {
            summory += listPoints[i].fightPower;
        }

        for (int i = 0; i < listPoints.Count; i++) {
            MapPoint mp = new MapPoint();
            mp.fightPower = (int) (missionMap.cost * listPoints[i].fightPower / summory);
            mp.count = generateCount(listPoints[i].type);
            mp.id = i;
            mp.isFinal = (i == listPoints.Count -1);

            if (i > 0) {
                mp.dependList.Add((i - 1).ToString());
            }
            mp.isEnable = (mp.dependList.Count == 0);
            mp.position = new Vector2(i + 1, 1);
            missionMap.fights.Add(mp);
        }
        return missionMap;
    }

    public MissionMap loadMissionFromFile(string link) {
        MissionMap missionMap = new MissionMap();
        TextAsset textAsset = (TextAsset)Resources.Load(link);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        XmlNode map = xmldoc.GetElementsByTagName("map").Item(0);
        foreach (XmlNode xmlMapPoint in map) {
            MapPoint mapPoint = new MapPoint();
            mapPoint.id = int.Parse(xmlMapPoint["id"].InnerText);
            mapPoint.fightPower = int.Parse(xmlMapPoint["fightPower"].InnerText);

            mapPoint.count = int.Parse(xmlMapPoint["count"].InnerText);

            if (xmlMapPoint["final"] != null
                && (xmlMapPoint["final"].InnerText == "true")) {
                mapPoint.isFinal = true;
            } else {
                mapPoint.isFinal = false;
            }
            if (xmlMapPoint["dependList"].InnerText.Length > 0) {
                mapPoint.dependList.AddRange(xmlMapPoint["dependList"].InnerText.Split(','));
            }

            mapPoint.position = XMLFactory.laodPosition(xmlMapPoint["position"]);
            if (mapPoint.dependList.Count == 0) {
                mapPoint.isEnable = true;
            }
            missionMap.fights.Add(mapPoint);
        }
        return missionMap;
    }


    public static int generateCount(MapPointType mpt) {
        if (MapPointType.BOSS.Equals(mpt)) {
            return 1;
        }
        if (MapPointType.SKIRMISH.Equals(mpt)) {
            return Random.Range(2, 4);
        }
        if (MapPointType.ARMY.Equals(mpt)) {
            return Random.Range(4, 6);
        }
        return 0;
    }

    public static MapPointType parseMapPoint(string name) {
        if ("BOSS".Equals(name)) {
            return MapPointType.BOSS;
        }
        if ("SKIRMISH".Equals(name)) {
            return MapPointType.SKIRMISH;
        }
        if ("ARMY".Equals(name)) {
            return MapPointType.ARMY;
        }
        return MapPointType.SKIRMISH;
    }
}
