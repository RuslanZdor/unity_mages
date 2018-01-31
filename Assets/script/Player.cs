using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Person {

    public Player() : base() {
        mana = int.MaxValue;
        health = int.MaxValue;
        isAlive = true;

        ally = AbilityTargetType.FRIEND;
        enemy = AbilityTargetType.ENEMY;

        name = "Player";
    }

    public override float eventStart(Ability ability, float eventStartTime) {
        float time = 0.0f;
        if (isAlive) {
            if (ability.GetType() != typeof(ActiveBuff)) {
                generateCooldownEvent(ability, eventStartTime);
            }
            time = ability.eventStart(eventStartTime);
            ability.playerCastCount--;
        }
        return time;
    }
}
