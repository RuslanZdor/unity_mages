using UnityEngine;
using System.Collections;

public class PartiesSingleton {
    public static Party heroes = new Party(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
    public static Party enemies = new Party(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);

    public static bool hasWinner() {
        if (heroes.allDead() || enemies.allDead()) {
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
}
