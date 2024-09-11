using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Inventory
    {
        // Creata a dectionary to store the items and their amount
        // Key: ItemData, Value: Amount (uint)
        // Use uint so that the amount can't be negative
        private Dictionary<ItemData, uint> _items = new Dictionary<ItemData, uint>();

        public bool Add(ItemData item, uint amount)
        {
            bool exists = _items.ContainsKey(item);

            if (exists)
            {
                _items[item] += amount;
            }
            else
            {
                _items.Add(item, amount);
            }

            Debug.Log($"Added item {item.Name}, amount: {amount}.");
			Debug.Log($"Item {item.Name} total number: {_items[item]}.");

            return true;
        }
    }
}
