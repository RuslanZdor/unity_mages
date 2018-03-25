using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class GameController : MonoBehaviour, IListenerObject {

    public static string TIME_FORMAT = "hh:mm:ss";

    private string userName;
    private DateTime gameTime;
    private float startGame;
    private string saveLink;

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

    public GameObject navigation;

    // Use this for initialization
    void Start() {
//        Instantiate(navigation);

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

        GameObject.Find("MessageController").GetComponent<MessageController>()
            .addMessage(new GameMessage(MessageType.OPEN_LOAD_GAME));

        GameObject.Find("MessageController").GetComponent<MessageController>().addListener(this);
    }

    private void loadSavedGame(string link) {
        saveLink = link;
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(link);
        XmlNode loadGame = xmldoc["savedGame"];

        userName = loadGame["userName"].InnerText;
        PartiesSingleton.gold = int.Parse(loadGame["gold"].InnerText);
        gameTime = DateTime.ParseExact(loadGame["time"].InnerText, TIME_FORMAT, System.Globalization.CultureInfo.CurrentCulture);
        startGame = Time.time;

        foreach (XmlNode xmlPerson in loadGame["persons"]) {
            Person person = XMLFactory.loadPerson(xmlPerson["type"].InnerText);
            person.name = xmlPerson["name"].InnerText;
            person.isActive = xmlPerson["isActive"].InnerText.Equals("True");
            person.setLevel(int.Parse(xmlPerson["level"].InnerText));
            person.setExpirience(int.Parse(xmlPerson["experience"].InnerText));

            person.itemList.Clear();
            foreach (XmlNode xmlItem in xmlPerson["items"]) {
                person.itemList.Add(XMLFactory.loadItem(xmlItem.InnerText));
            }

            person.knownAbilities.FindAll((Ability ability) =>
                    ability.resource != null && ability.resource != "")
                .ForEach((Ability ability) => ability.isActive = false);

            foreach (XmlNode xmlAbility in xmlPerson["activeAbilities"]) {
                person.findAbility(xmlAbility.InnerText).isActive = true;
            }

            person.place = XMLFactory.laodPosition(xmlPerson["position"]);

            PartiesSingleton.selectedHeroes.Add(person);
        }

        foreach (XmlNode xmlItem in loadGame["inventory"]) {
            PartiesSingleton.inventory.Add(XMLFactory.loadItem(xmlItem.InnerText));
        }
    }

    private void saveGame() {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmlSavedGame = xmldoc.CreateElement("savedGame");

        xmldoc.AppendChild(xmlSavedGame);

        xmlSavedGame.AppendChild(xmldoc.CreateElement("userName")).InnerText = userName;
        xmlSavedGame.AppendChild(xmldoc.CreateElement("gold")).InnerText = PartiesSingleton.gold.ToString();
        gameTime = gameTime.AddSeconds(Time.time - startGame);
        startGame = Time.time;
        xmlSavedGame.AppendChild(xmldoc.CreateElement("time")).InnerText = gameTime.ToString(TIME_FORMAT);

        saveInventory(xmldoc, PartiesSingleton.inventory);
        savePersons(xmldoc, PartiesSingleton.selectedHeroes);

        XmlElement xmlPersons = xmldoc.CreateElement("persons");

        xmldoc.PreserveWhitespace = true;
        xmldoc.Save(saveLink);
    }

    private void saveInventory(XmlDocument xmldoc, List<Item> inventory) {
        XmlElement xmlInventory = xmldoc.CreateElement("inventory");
        foreach (Item item in inventory) {
            XmlElement xmlItem = xmldoc.CreateElement("item");
            xmlItem.InnerText = item.resource;
            xmlInventory.AppendChild(xmlItem);
        }
        xmldoc["savedGame"].AppendChild(xmlInventory);
    }

    private void savePersons(XmlDocument xmldoc, List<Person> persons) {
        XmlElement xmlPersons = xmldoc.CreateElement("persons");
        foreach (Person person in persons) {
            XmlElement xmlPerson = xmldoc.CreateElement("person");
            xmlPerson.AppendChild(xmldoc.CreateElement("type")).InnerText = person.resource;
            xmlPerson.AppendChild(xmldoc.CreateElement("name")).InnerText = person.name;
            xmlPerson.AppendChild(xmldoc.CreateElement("experience")).InnerText = person.experience.ToString();
            xmlPerson.AppendChild(xmldoc.CreateElement("level")).InnerText = person.level.ToString();
            xmlPerson.AppendChild(xmldoc.CreateElement("isActive")).InnerText = person.isActive.ToString();

            xmlPerson.AppendChild(xmldoc.CreateElement("position"));
            xmlPerson["position"].AppendChild(xmldoc.CreateElement("x")).InnerText = person.place.x.ToString();
            xmlPerson["position"].AppendChild(xmldoc.CreateElement("y")).InnerText = person.place.y.ToString();

            xmlPerson.AppendChild(xmldoc.CreateElement("items"));
            foreach (Item item in person.itemList) {
                xmlPerson["items"].AppendChild(xmldoc.CreateElement("item")).InnerText = item.resource;
            }

            xmlPerson.AppendChild(xmldoc.CreateElement("activeAbilities"));
            foreach (Ability ability in person.knownAbilities.FindAll(
                (Ability ability) => ability.isActive
                && ability.resource != null && ability.resource != "")) {
                xmlPerson["activeAbilities"].AppendChild(xmldoc.CreateElement("ability")).InnerText = ability.resource;
            }

            xmlPersons.AppendChild(xmlPerson);
        }
        xmldoc["savedGame"].AppendChild(xmlPersons);
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.LOAD_SAVED_GAME) {
            loadSavedGame((string) message.parameters[0]);
            GameObject.Find("Navigation").GetComponent<NavigationController>().generateShop();
        }
        if (message.type == MessageType.START_NEW_GAME) {
            saveLink = (string) message.parameters[1];
            userName = (string) message.parameters[0];
            PartiesSingleton.gold = 0;
            gameTime = DateTime.ParseExact("00:00:00", TIME_FORMAT, System.Globalization.CultureInfo.CurrentCulture);
            startGame = Time.time;

            GameObject.Find("Navigation").GetComponent<NavigationController>().openMainMenu();
            GameObject.Find("Navigation").GetComponent<NavigationController>().generateShop();
        }
        if (message.type == MessageType.SAVE_GAME) {
            saveGame();
        }
        if (message.type == MessageType.GENERATE_SHOP) {
            PartiesSingleton.currentShop = new Shop();
            PartiesSingleton.currentShop.gold = 100;
            PartiesSingleton.currentShop.items = new List<Item>();

            PartiesSingleton.currentShop.items.Add(XMLFactory.loadItem("configs/items/weapons/mage_stuff"));
            PartiesSingleton.currentShop.items.Add(XMLFactory.loadItem("configs/items/shields/shield"));
        }
    }
}
