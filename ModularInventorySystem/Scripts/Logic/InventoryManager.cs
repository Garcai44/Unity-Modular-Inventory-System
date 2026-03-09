using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Core logic for the Modular Inventory System.
/// Handles adding, removing, and stacking items without touching the UI directly.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    [Tooltip("Total number of slots available in this inventory.")]
    public int inventorySize = 20;

    [Header("Runtime Data (Do not modify manually)")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    [Header("Events")]
    [Tooltip("Fired whenever an item is added, removed, or changed. UI should listen to this.")]
    public UnityEvent OnInventoryChanged;

    private void Awake()
    {
        InitializeInventory();
    }

    /// <summary>
    /// Fills the inventory with empty slots up to the maximum size.
    /// </summary>
    private void InitializeInventory()
    {
        slots = new List<InventorySlot>(inventorySize);
        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    /// <summary>
    /// Attempts to add an item to the inventory. Handles stacking and splitting if necessary.
    /// </summary>
    /// <param name="itemToAdd">The ItemData blueprint to add.</param>
    /// <param name="amountToAdd">Quantity to add.</param>
    /// <returns>True if all items were added. False if the inventory became full.</returns>
    public bool AddItem(ItemData itemToAdd, int amountToAdd)
    {
        if (itemToAdd == null || amountToAdd <= 0) return false;

        // 1. If the item is stackable, try to fill existing partially-filled slots first
        if (itemToAdd.isStackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (!slot.IsEmpty() && slot.item == itemToAdd && slot.amount < itemToAdd.maxStackSize)
                {
                    int spaceLeftInSlot = itemToAdd.maxStackSize - slot.amount;
                    
                    if (amountToAdd <= spaceLeftInSlot)
                    {
                        // Fits perfectly in this slot
                        slot.AddAmount(amountToAdd);
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                    else
                    {
                        // Fills this slot, but we still have leftover items to add
                        slot.AddAmount(spaceLeftInSlot);
                        amountToAdd -= spaceLeftInSlot;
                    }
                }
            }
        }

        // 2. Find empty slots for the remaining amount (or for non-stackable items)
        while (amountToAdd > 0)
        {
            InventorySlot emptySlot = GetFirstEmptySlot();
            
            if (emptySlot == null)
            {
                // No more space in the inventory!
                Debug.LogWarning("Inventory is full!");
                OnInventoryChanged?.Invoke();
                return false; 
            }

            if (itemToAdd.isStackable)
            {
                // Take as much as we can fit in one slot
                int amountForThisSlot = Mathf.Min(amountToAdd, itemToAdd.maxStackSize);
                emptySlot.item = itemToAdd;
                emptySlot.amount = amountForThisSlot;
                amountToAdd -= amountForThisSlot;
            }
            else
            {
                // Non-stackable items ALWAYS take exactly 1 slot, no matter what
                emptySlot.item = itemToAdd;
                emptySlot.amount = 1;
                amountToAdd -= 1;
            }
        }

        OnInventoryChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// Removes a specific amount of an item directly from a targeted memory reference (slot).
    /// Prevents removing identical items from the wrong backpack position.
    /// </summary>
    /// <param name="targetSlot">The specific slot instance clicked by the user.</param>
    /// <param name="amountToRemove">Quantity to deduct.</param>
    public void RemoveItemFromSlot(InventorySlot targetSlot, int amountToRemove)
    {
        if (targetSlot == null || targetSlot.IsEmpty() || amountToRemove <= 0) return;

        if (amountToRemove >= targetSlot.amount)
        {
            // Remove the whole stack
            targetSlot.Clear();
        }
        else
        {
            // Remove only a portion
            targetSlot.RemoveAmount(amountToRemove);
        }

        // Notify the UI to refresh the visuals
        OnInventoryChanged?.Invoke();
    }

    /// <summary> Helper to find the first available empty slot. </summary>
    private InventorySlot GetFirstEmptySlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }
}