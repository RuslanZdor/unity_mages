public class PersonStatistics {
    public float damageDealed = 0.0f;
    public float heal = 0.0f;
    public float damageTaken = 0.0f;

    public int buffCount = 0;
    public int debuffCount = 0;
    public int summonCount = 0;

    public void reset() {
        damageDealed = 0.0f;
        heal = 0.0f;
        damageTaken = 0.0f;

        buffCount = 0;
        debuffCount = 0;
        summonCount = 0;
    }
}