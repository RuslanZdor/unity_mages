using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constants {

    public static Sprite loadSprite(string source, string name) {
        Sprite image;
        List<Sprite> images = new List<Sprite>();
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
    public static  int PERSON_HEALTH_PER_LEVEL = 0;
    public static  int PERSON_MANA_PER_LEVEL = 0;
    public static  int PERSON_AGRO = 10;
    public static  int PERSON_MELEE_ATTACK_SPEED = 1;
    public static  int PERSON_MELEE_ATTACK_DAMAGE = 1;

    public static  int BASE_MAGE_HEALTH = 100;
    public static  int BASE_MAGE_MANA = 20;
    public static  int BASE_MAGE_HEALTH_PER_LEVEL = 5;
    public static  int BASE_MAGE_MANA_PER_LEVEL = 1;

 }
