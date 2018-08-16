using System;
using System.Collections.Generic;

namespace script.game.common {
    public class Game {
        public string saveLink;
        public string userName;
        public DateTime gameTime;
        public float startGame;
        public float gold;
        
        public List<Person> activeHeroes = new List<Person>();
        public List<Person> selectedHeroes = new List<Person>();

        public List<Item> inventory = new List<Item>();

    }
}