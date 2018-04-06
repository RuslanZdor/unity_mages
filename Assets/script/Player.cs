namespace script
{
    public class Player : Person {

        public Player() {
            mana = int.MaxValue;
            health = int.MaxValue;
            isAlive = true;

            ally = AbilityTargetType.FRIEND;
            enemy = AbilityTargetType.ENEMY;

            name = "Player";
        }

        public override float eventStart(Ability ability, float eventStartTime) {
            var time = 0.0f;
        
            if (!isAlive) return time;
        
            if (ability.GetType() != typeof(ActiveBuff)) {
                generateCooldownEvent(ability, eventStartTime);
            }        
            time = ability.eventStart(eventStartTime);
            ability.playerCastCount--;
            return time;
        }
    }
}
