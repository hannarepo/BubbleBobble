using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace BubbleBobble
{
	/// <summary>
	/// Inventory class for storing items and their amounts.
	/// Only stores shells, because they are the only items
	/// used in the shop.
	/// </summary>
	///
	/// <remarks>
	/// author: Hanna Repo
	/// </remarks>

	public class Inventory
	{
		// Creat a dectionary to store the items and their amount
		// Key: ItemData, Value: Amount (uint)
		// Use uint so that the amount can't be negative
		private Dictionary<ItemData, uint> _items = new Dictionary<ItemData, uint>();

		/// <summary>
		/// Add items to inventory.
		/// </summary>
		/// <param name="item"> Data of the item to be added, containing a scriptable object. </param>
		/// <param name="amount"> The number of items to be added. </param>
		/// <returns></returns>
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
			return true;
		}

		/// <summary>
		/// Remove items from inventory.
		/// </summary>
		/// <param name="item"> Data of the item to be removed, containing a scriptable object. </param>
		/// <param name="amount"> The number of items to be removed. </param>
		public bool Remove(ItemData item, uint amount)
		{
			if (!_items.ContainsKey(item) || _items[item] < amount)
			{
				return false;
			}

			if (_items[item] == amount)
			{
				_items.Remove(item);
			}
			else
			{
				_items[item] -= amount;
			}
			return true;
		}

		public void Clear()
		{
			_items.Clear();
		}

		public bool CheckInventoryContent(ItemData item, int amount)
		{
			if (_items.ContainsKey(item) && _items[item] >= amount)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Count(int count)
		{
			if (_items.Count >= count)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
