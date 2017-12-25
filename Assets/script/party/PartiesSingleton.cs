using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartiesSingleton {
    public static Party heroes = new Party(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
    public static Party enemies = new Party(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);

    public static List<Person> activeHeroes = new List<Person>();
    public static List<Item> inventory = new List<Item>();

    public static bool hasWinner() {
        if (heroes.allDead() || enemies.allDead()) {
            return true;
        }
        return false;
    }

    public static bool isHeroesWinner() {
        if (hasWinner() && enemies.allDead()) {
            return true;
        }
        return false;
    }

    public static Party getParty(AbilityTargetType type) {
        if (type.Equals(AbilityTargetType.ENEMY)) {
            return enemies;
        }else {
            return heroes;
        }
    }

    public static void clear() {
        heroes.getPartyList().Clear();
        enemies.getPartyList().Clear();
    }
}
