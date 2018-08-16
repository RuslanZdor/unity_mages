using System.Collections.Generic;
using script;
using script.game.common;
using UnityEngine;

public class PartiesSingleton {

    public static Shop currentShop;
    public static Game currentGame;
    
    public static Party heroes = new Party(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
    public static Party enemies = new Party(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);

    public static Player player = new Player();

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
        }
        return heroes;
    }

    public static void clear() {
        heroes.getPartyList().Clear();
        enemies.getPartyList().Clear();
        player.abilityList.Clear();
    }

    public static Vector2 generatePlace() {
        var list = new List<Vector2>();
        for (int i = 1; i < 3; i++) {
            for (int j = 1; j < 4; j++) {
                if (isPlaceEmpty(i, j)) {
                    list.Add(new Vector2(i, j));
                }
            }
        }
        if (list.Count > 0) {
            return list[Random.Range(0, list.Count)];
        }
        return new Vector2();
    }

    public static bool isPlaceEmpty(int row, int index) {
        foreach (var go in currentGame.activeHeroes) {
            if (go.place.x == row
                && go.place.y == index) {
                return false;
            }
        }
        return true;
    }
}
