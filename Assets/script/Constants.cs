using System.Collections.Generic;
using UnityEngine;

namespace script
{
    public static class Constants {

        public static Sprite loadSprite(string source, string name) {
            Sprite image;
            var images = new List<Sprite>();
            images.AddRange(Resources.LoadAll<Sprite>(source));
            image = images.Find(s => s.name == name);
            return image;
        }

        public static float LEVEL_MULTIPLAYER = 1.1f;
        public static float getMultiplayer(int level) {
            float result = 1;
            for (int i = 1; i < level; i++) {
                result *= LEVEL_MULTIPLAYER;
            }
            return result;
        }

        /*Person parameters*/
        public static int PERSON_BASE_HEALTH = 11;
        public static  int PERSON_BASE_MANA = 0;
        public static  int PERSON_AGRO = 10;
        public static  int PERSON_MELEE_ATTACK_SPEED = 1;

        public static readonly string NAVIGATION_OBJECT = "Navigation";
        public static readonly string MESSAGE_CONTROLLER_OBJECT = "MessageController";
        public static readonly string BACKGROUND = "Background";
    }
}
