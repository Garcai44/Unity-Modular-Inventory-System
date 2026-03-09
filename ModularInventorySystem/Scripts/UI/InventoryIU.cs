using UnityEngine;

/// <summary>
/// Listens to the InventoryManager and updates all UI slots accordingly.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    public InventoryManager inventoryManager;
    
    [Tooltip("The parent object holding all the InventorySlotUI GameObjects.")]
    public Transform slotsParent;

    private InventorySlotUI[] uiSlots;

    private void Start()
    {
        // Find all UI slots dynamically at start
        uiSlots = slotsParent.GetComponentsInChildren<InventorySlotUI>();

        // Optional: Initial update just in case the inventory starts with items
        UpdateUI();
    }

    /// <summary>
    /// Loops through all slots in the manager and updates the corresponding UI slot.
    /// This method should be called by the OnInventoryChanged UnityEvent.
    /// </summary>
    public void UpdateUI()
    {
        if (inventoryManager == null || uiSlots == null) return;

        for (int i = 0; i < uiSlots.Length; i++)
        {
            // If the backend inventory has a slot for this UI element, update it
            if (i < inventoryManager.slots.Count)
            {
                uiSlots[i].UpdateSlot(inventoryManager.slots[i]);
            }
            else
            {
                // If we have more UI slots than actual inventory capacity, clear the excess
                uiSlots[i].ClearSlot();
            }
        }
    }
}