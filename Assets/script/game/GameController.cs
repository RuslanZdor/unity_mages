using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using script;
using script.game;
using UnityEngine;

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

    // Use this for initialization
    void Start() {
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

        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>()
            .addMessage(new GameMessage(MessageType.OPEN_LOAD_GAME));

        GameObject.Find(Constants.MESSAGE_CONTROLLER_OBJECT).GetComponent<MessageController>().addListener(this);
    }

    private void loadSavedGame(string link) {
        saveLink = link;
        var xmldoc = new XmlDocument();
        xmldoc.Load(link);
        XmlNode loadGame = xmldoc["savedGame"];

        userName = loadGame["userName"].InnerText;
        PartiesSingleton.gold = int.Parse(loadGame["gold"].InnerText);
        gameTime = DateTime.ParseExact(loadGame["time"].InnerText, TIME_FORMAT, CultureInfo.CurrentCulture);
        startGame = Time.time;

        foreach (XmlNode xmlPerson in loadGame["persons"]) {
            var person = XMLFactory.loadPerson(xmlPerson["type"].InnerText);
            person.name = xmlPerson["name"].InnerText;
            person.isActive = xmlPerson["isActive"].InnerText.Equals("True");
            person.setLevel(int.Parse(xmlPerson["level"].InnerText));
            person.setExpirience(int.Parse(xmlPerson["experience"].InnerText));

            person.itemList.Clear();
            foreach (XmlNode xmlItem in xmlPerson["items"]) {
                person.itemList.Add(XMLFactory.loadItem(xmlItem.InnerText));
            }

            person.knownAbilities.FindAll(ability =>
                    !string.IsNullOrEmpty(ability.resource))
                .ForEach(ability => ability.isActive = false);

            foreach (XmlNode xmlAbility in xmlPerson["activeAbilities"]) {
                person.findAbility(xmlAbility.InnerText).isActive = true;
            }

            person.place = XMLFactory.laodPosition(xmlPerson["position"]);

            PartiesSingleton.selectedHeroes.Add(person);
        }

        foreach (XmlNode xmlItem in loadGame[InventoryController.INVENTORY_OBJECT]) {
            PartiesSingleton.inventory.Add(XMLFactory.loadItem(xmlItem.InnerText));
        }
    }

    private void saveGame() {
        var xmldoc = new XmlDocument();
        var xmlSavedGame = xmldoc.CreateElement("savedGame");

        xmldoc.AppendChild(xmlSavedGame);

        xmlSavedGame.AppendChild(xmldoc.CreateElement("userName")).InnerText = userName;
        xmlSavedGame.AppendChild(xmldoc.CreateElement("gold")).InnerText = PartiesSingleton.gold.ToString();
        gameTime = gameTime.AddSeconds(Time.time - startGame);
        startGame = Time.time;
        xmlSavedGame.AppendChild(xmldoc.CreateElement("time")).InnerText = gameTime.ToString(TIME_FORMAT);

        saveInventory(xmldoc, PartiesSingleton.inventory);
        savePersons(xmldoc, PartiesSingleton.selectedHeroes);

        xmldoc.PreserveWhitespace = true;
        xmldoc.Save(saveLink);
    }

    private void saveInventory(XmlDocument xmldoc, List<Item> inventory) {
        var xmlInventory = xmldoc.CreateElement(InventoryController.INVENTORY_OBJECT);
        foreach (var item in inventory) {
            var xmlItem = xmldoc.CreateElement("item");
            xmlItem.InnerText = item.resource;
            xmlInventory.AppendChild(xmlItem);
        }
        xmldoc["savedGame"].AppendChild(xmlInventory);
    }

    private void savePersons(XmlDocument xmldoc, List<Person> persons) {
        var xmlPersons = xmldoc.CreateElement("persons");
        foreach (var person in persons) {
            var xmlPerson = xmldoc.CreateElement("person");
            xmlPerson.AppendChild(xmldoc.CreateElement("type")).InnerText = person.resource;
            xmlPerson.AppendChild(xmldoc.CreateElement("name")).InnerText = person.name;
            xmlPerson.AppendChild(xmldoc.CreateElement("experience")).InnerText = person.experience.ToString();
            xmlPerson.AppendChild(xmldoc.CreateElement("level")).InnerText = person.level.ToString();
            xmlPerson.AppendChild(xmldoc.CreateElement("isActive")).InnerText = person.isActive.ToString();

            xmlPerson.AppendChild(xmldoc.CreateElement("position"));
            xmlPerson["position"].AppendChild(xmldoc.CreateElement("x")).InnerText = person.place.x.ToString();
            xmlPerson["position"].AppendChild(xmldoc.CreateElement("y")).InnerText = person.place.y.ToString();

            xmlPerson.AppendChild(xmldoc.CreateElement("items"));
            foreach (var item in person.itemList) {
                xmlPerson["items"].AppendChild(xmldoc.CreateElement("item")).InnerText = item.resource;
            }

            xmlPerson.AppendChild(xmldoc.CreateElement("activeAbilities"));
            foreach (var ability in person.knownAbilities.FindAll(
                ability => ability.isActive
                && ability.resource != null && ability.resource != "")) {
                xmlPerson["activeAbilities"].AppendChild(xmldoc.CreateElement("ability")).InnerText = ability.resource;
            }

            xmlPersons.AppendChild(xmlPerson);
        }
        xmldoc["savedGame"].AppendChild(xmlPersons);
    }

    public void readMessage(GameMessage message) {
        switch (message.type) {
            case MessageType.LOAD_SAVED_GAME:
                loadSavedGame((string) message.parameters[0]);
                GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().generateShop();
                break;
            case MessageType.START_NEW_GAME:
                saveLink = (string) message.parameters[1];
                userName = (string) message.parameters[0];
                PartiesSingleton.gold = 0;
                gameTime = DateTime.ParseExact("00:00:00", TIME_FORMAT, CultureInfo.CurrentCulture);
                startGame = Time.time;

                GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().openMainMenu();
                GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().generateShop();
                break;
            case MessageType.SAVE_GAME:
                saveGame();
                break;
            case MessageType.GENERATE_SHOP:
                PartiesSingleton.currentShop = new Shop {
                    gold = 100,
                    items = new List<Item> {
                        XMLFactory.loadItem("configs/items/weapons/mage_stuff"),
                        XMLFactory.loadItem("configs/items/shields/shield")
                    }
                };
                break;
        }
    }
}
