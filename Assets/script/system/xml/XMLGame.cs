using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;
using script.game.common;
using UnityEngine;

namespace script.system.xml {
    
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class XMLGame {

        //xml structure
        public const string XML_SAVED_GAME = "SavedGame";
        public const string XML_USER_NAME = "UserName";
        public const string XML_GOLD = "Gold";
        public const string XML_TIME = "Time";
        public const string XML_INVENTORY_ITEM = "Item";
        public const string XML_PERSONS = "Persons";
        public const string XML_PERSON = "Person";
        public const string XML_PERSON_TYPE = "Type";
        public const string XML_PERSON_NAME = "Name";
        public const string XML_PERSON_EXPERIENCE = "Experience";
        public const string XML_PERSON_LEVEL = "Level";
        public const string XML_PERSON_ISACTIVE = "IsActive";
        public const string XML_PERSON_POSITION = "Position";
        public const string XML_PERSON_POSITION_X = "X";
        public const string XML_PERSON_POSITION_Y = "Y";
        public const string XML_PERSON_ACTIVE_ABILITIES = "ActiveAbilities";
        public const string XML_PERSON_ABILITY = "Ability";
        public const string XML_PERSON_ACTIVE_ITEMS = "ActiveItems";
        public const string XML_PERSON_ITEMS_LEFT_HAND = "LeftHand";
        public const string XML_PERSON_ITEMS_RIGHT_HAND = "RightHand";
        public const string XML_PERSON_ITEMS_ARMOR = "Armor";
        public const string XML_PERSON_ITEMS_ACTIVE_ITEM = "ActiveItem";
        
        private static XMLGame instance;
        private const string TIME_FORMAT = "hh:mm:ss";

        public static XMLGame getInstance() {
            return instance ?? (instance = new XMLGame());
        }

        public Game createNewGame() {
            var game = new Game {
                gold = 0,
                gameTime = DateTime.ParseExact("00:00:00", TIME_FORMAT, CultureInfo.CurrentCulture),
                startGame = Time.time
            };
            return game;
        }
        
        public Game loadSavedGame(string link) {
            var xmldoc = new XmlDocument();
            xmldoc.Load(link);
            XmlNode loadGame = xmldoc[XML_SAVED_GAME];

            var game = new Game {
                saveLink = link,
                userName = loadGame[XML_USER_NAME].InnerText,
                gold = int.Parse(loadGame[XML_GOLD].InnerText),
                startGame = Time.time,
                gameTime = DateTime.ParseExact(loadGame[XML_TIME].InnerText, TIME_FORMAT, CultureInfo.CurrentCulture)
            };

            foreach (XmlNode xmlPerson in loadGame[XML_PERSONS]) {
                var person = XMLFactory.loadPerson(xmlPerson[XML_PERSON_TYPE].InnerText);
                person.name = xmlPerson[XML_PERSON_NAME].InnerText;
                person.isActive = xmlPerson[XML_PERSON_ISACTIVE].InnerText.Equals(true.ToString());
                person.setLevel(int.Parse(xmlPerson[XML_PERSON_LEVEL].InnerText));
                person.setExpirience(int.Parse(xmlPerson[XML_PERSON_EXPERIENCE].InnerText));
    
                person.activeInventory.leftHand = XMLFactory.loadItem(xmlPerson[XML_PERSON_ACTIVE_ITEMS][XML_PERSON_ITEMS_LEFT_HAND].InnerText);
                person.activeInventory.rightHand = XMLFactory.loadItem(xmlPerson[XML_PERSON_ACTIVE_ITEMS][XML_PERSON_ITEMS_RIGHT_HAND].InnerText);
                person.activeInventory.armor = XMLFactory.loadItem(xmlPerson[XML_PERSON_ACTIVE_ITEMS][XML_PERSON_ITEMS_ARMOR].InnerText);
                person.activeInventory.activeItem = XMLFactory.loadItem(xmlPerson[XML_PERSON_ACTIVE_ITEMS][XML_PERSON_ITEMS_ACTIVE_ITEM].InnerText);
                
                person.knownAbilities.FindAll(ability =>
                        !string.IsNullOrEmpty(ability.resource))
                    .ForEach(ability => ability.isActive = false);
    
                foreach (XmlNode xmlAbility in xmlPerson[XML_PERSON_ACTIVE_ABILITIES]) {
                    person.findAbility(xmlAbility.InnerText).isActive = true;
                }
    
                person.place = XMLFactory.laodPosition(xmlPerson[XML_PERSON_POSITION]);
    
                game.selectedHeroes.Add(person);
            }
    
            foreach (XmlNode xmlItem in loadGame[InventoryController.INVENTORY_OBJECT]) {
                game.inventory.Add(XMLFactory.loadItem(xmlItem.InnerText));
            }

            return game;
        }

        public void saveGame(Game game) {
            var xmldoc = new XmlDocument();
            var xmlSavedGame = xmldoc.CreateElement(XML_SAVED_GAME);
    
            xmldoc.AppendChild(xmlSavedGame);
    
            xmlSavedGame.AppendChild(xmldoc.CreateElement(XML_USER_NAME)).InnerText = game.userName;
            xmlSavedGame.AppendChild(xmldoc.CreateElement(XML_GOLD)).InnerText = game.gold.ToString();
            game.gameTime = game.gameTime.AddSeconds(Time.time - game.startGame);
            game.startGame = Time.time;
            xmlSavedGame.AppendChild(xmldoc.CreateElement(XML_TIME)).InnerText = game.gameTime.ToString(TIME_FORMAT);
    
            saveInventory(xmldoc, game.inventory);
            savePersons(xmldoc, game.selectedHeroes);
    
            xmldoc.PreserveWhitespace = true;
            xmldoc.Save(game.saveLink);
        }

        private void saveInventory(XmlDocument xmldoc, List<Item> inventory) {
            var xmlInventory = xmldoc.CreateElement(InventoryController.INVENTORY_OBJECT);
            foreach (var item in inventory) {
                var xmlItem = xmldoc.CreateElement(XML_INVENTORY_ITEM);
                xmlItem.InnerText = item.resource;
                xmlInventory.AppendChild(xmlItem);
            }
            xmldoc[XML_SAVED_GAME].AppendChild(xmlInventory);
        }

        private void savePersons(XmlDocument xmldoc, IEnumerable<Person> persons) {
            var xmlPersons = xmldoc.CreateElement(XML_PERSONS);
            foreach (var person in persons) {
                var xmlPerson = xmldoc.CreateElement(XML_PERSON);
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_TYPE)).InnerText = person.resource;
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_NAME)).InnerText = person.name;
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_EXPERIENCE)).InnerText = person.experience.ToString();
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_LEVEL)).InnerText = person.level.ToString();
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_ISACTIVE)).InnerText = person.isActive.ToString();
    
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_POSITION));
                xmlPerson[XML_PERSON_POSITION].AppendChild(xmldoc.CreateElement(XML_PERSON_POSITION_X)).InnerText = person.place.x.ToString();
                xmlPerson[XML_PERSON_POSITION].AppendChild(xmldoc.CreateElement(XML_PERSON_POSITION_Y)).InnerText = person.place.y.ToString();
    
                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_ACTIVE_ITEMS));
                
                xmlPerson[XML_PERSON_ACTIVE_ITEMS].AppendChild(xmldoc.CreateElement(XML_PERSON_ITEMS_LEFT_HAND)).InnerText = 
                    person.activeInventory.leftHand != null ? person.activeInventory.leftHand.resource : "";
                xmlPerson[XML_PERSON_ACTIVE_ITEMS].AppendChild(xmldoc.CreateElement(XML_PERSON_ITEMS_RIGHT_HAND)).InnerText = 
                    person.activeInventory.rightHand != null ? person.activeInventory.rightHand.resource : "";
                xmlPerson[XML_PERSON_ACTIVE_ITEMS].AppendChild(xmldoc.CreateElement(XML_PERSON_ITEMS_ARMOR)).InnerText = 
                    person.activeInventory.armor != null ? person.activeInventory.armor.resource : "";
                xmlPerson[XML_PERSON_ACTIVE_ITEMS].AppendChild(xmldoc.CreateElement(XML_PERSON_ITEMS_ACTIVE_ITEM)).InnerText = 
                    person.activeInventory.activeItem != null ? person.activeInventory.activeItem.resource : "";

                xmlPerson.AppendChild(xmldoc.CreateElement(XML_PERSON_ACTIVE_ABILITIES));
                foreach (var ability in person.knownAbilities.FindAll(
                    ability => ability.isActive
                    && !string.IsNullOrEmpty(ability.resource))) {
                    xmlPerson[XML_PERSON_ACTIVE_ABILITIES].AppendChild(xmldoc.CreateElement(XML_PERSON_ABILITY)).InnerText = ability.resource;
                }
    
                xmlPersons.AppendChild(xmlPerson);
            }
            xmldoc[XML_SAVED_GAME].AppendChild(xmlPersons);
        }

    }
}