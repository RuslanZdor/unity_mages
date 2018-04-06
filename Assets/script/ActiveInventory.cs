using System.Collections.Generic;

namespace script {
    public class ActiveInventory {
        public Item rightHand;
        public Item leftHand;
        public Item armor;
        public Item activeItem;

        public List<Item> getItemList() {
            List<Item> items = new List<Item> {rightHand, leftHand, armor, activeItem};
            return items;
        }

        public void destroy(Item item) {
            
        }
    }
}