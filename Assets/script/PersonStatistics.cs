namespace script
{
    public class PersonStatistics {
        public float damageDealed;
        public float heal;
        public float damageTaken;

        public void reset() {
            damageDealed = 0.0f;
            heal = 0.0f;
            damageTaken = 0.0f;
        }
    }
}