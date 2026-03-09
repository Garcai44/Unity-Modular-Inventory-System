using UnityEngine;

/// <summary>
/// Represents a single slot in the inventory holding an Item and its quantity.
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    // Default empty constructor
    public InventorySlot()
    {
        item = null;
        amount = 0;
    }

    // Parameterized constructor initializing a specific item and amount
    public InventorySlot(ItemData newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }

    /// <summary> Checks if the slot is empty. </summary>
    public bool IsEmpty() => item == null || amount <= 0;

    /// <summary> Adds quantity to the current slot. </summary>
    public void AddAmount(int value)
    {
        amount += value;
    }

    /// <summary> Removes quantity and clears the slot if it reaches zero. </summary>
    public void RemoveAmount(int value)
    {
        amount -= value;
        if (amount <= 0)
        {
            Clear();
        }
    }

    /// <summary> Empties the slot completely. </summary>
    public void Clear()
    {
        item = null;
        amount = 0;
    }
}