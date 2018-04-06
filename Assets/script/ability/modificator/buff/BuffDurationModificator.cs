public class BuffDurationModificator : AbstractModificator{

    public float value;

    public BuffDurationModificator(float chance) {
        value =  chance;
    }
		
	public override void updateMakingBuff(Ability ability) {
        var buff = (Buff) ability;
        buff.duration = (int) buff.duration * (100 + value) / 100;
    }
}
