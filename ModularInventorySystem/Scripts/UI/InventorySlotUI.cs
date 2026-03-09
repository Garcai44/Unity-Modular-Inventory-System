using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the visual representation of a single inventory slot.
/// Responds to mouse hover and click events.
/// </summary>
public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")] [Tooltip("Reference to the main inventory manager to handle item consumption/removal.")]
    public InventoryManager manager;

    [Header("UI Components")] public Image iconImage;
    public TMP_Text amountText;

    private InventorySlot currentSlot;

    /// <summary> Updates the UI to show the item and amount. </summary>
    public void UpdateSlot(InventorySlot slot)
    {
        currentSlot = slot;

        if (slot == null || slot.IsEmpty())
        {
            ClearSlot();
            return;
        }

        iconImage.sprite = slot.item.icon;
        iconImage.color = Color.white;

        if (slot.amount > 1)
        {
            amountText.text = slot.amount.ToString();
            amountText.gameObject.SetActive(true);
        }
        else
        {
            amountText.gameObject.SetActive(false);
        }
    }

    /// <summary> Hides the icon and text when the slot is empty. </summary>
    public void ClearSlot()
    {
        currentSlot = null;
        iconImage.sprite = null;
        iconImage.color = new Color(1, 1, 1, 0);
        amountText.gameObject.SetActive(false);
    }

    // --- MOUSE HOVER EVENTS ---

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentSlot != null && !currentSlot.IsEmpty())
        {
            InventoryTooltip.Instance.ShowTooltip(currentSlot.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InventoryTooltip.Instance != null)
        {
            InventoryTooltip.Instance.HideTooltip();
        }
    }

    // --- MOUSE CLICK EVENTS ---
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentSlot == null || currentSlot.IsEmpty() || manager == null) return;

        // Left Click: Consume/Use the item
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (currentSlot.item.itemType == ItemType.Consumable)
            {
                Debug.Log($"Consumed: {currentSlot.item.displayName}!");

                // Targeted removal instead of generic search
                manager.RemoveItemFromSlot(currentSlot, 1);

                if (currentSlot == null || currentSlot.IsEmpty())
                {
                    InventoryTooltip.Instance.HideTooltip();
                }
            }
            else
            {
                Debug.Log($"Cannot consume this item: {currentSlot.item.displayName}");
            }
        }
        // Right Click: Drop/Trash the item
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Trashed: {currentSlot.item.displayName}");

            // Targeted removal instead of generic search
            manager.RemoveItemFromSlot(currentSlot, 1);

            if (currentSlot == null || currentSlot.IsEmpty())
            {
                InventoryTooltip.Instance.HideTooltip();
            }
        }
    }
}