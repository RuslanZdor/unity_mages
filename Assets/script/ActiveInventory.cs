using System.Collections.Generic;

namespace script {
    public class ActiveInventory {
        public Item rightHand;
        public Item leftHand;
        public Item armor;
        public Item activeItem;

        public List<Item> getItemList() {
            List<Item> items = new List<Item>();
            if (rightHand != null) {
                items.Add(rightHand);
            }
            if (leftHand != null) {
                items.Add(leftHand);
            }
            if (armor != null) {
                items.Add(armor);
            }
            if (activeItem != null) {
                items.Add(activeItem);
            }
            return items;
        }

        public void clear() {
            destroy(rightHand);
            destroy(leftHand);
            destroy(armor);
            destroy(activeItem);
        }

        public void destroy(Item item) {
            item = null;
        }
    }
}