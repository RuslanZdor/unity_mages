public class DestroyItemEvent : BasicTargetEvent {

	public Item item;

	public override float eventStart() {
        owner.activeInventory.destroy(item);
        logEvent(" item was destroied " + item.name);
        return 0.0f;
    }
}
