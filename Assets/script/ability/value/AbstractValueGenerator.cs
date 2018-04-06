public abstract class AbstractValueGenerator {

    public int level = 1;

    public abstract float getValue();
    public abstract void updateLevel(int level);
}
