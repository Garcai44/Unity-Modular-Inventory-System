using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; 

/// <summary>
/// Global tooltip manager for inventory items. 
/// Follows the mouse position and displays item data.
/// </summary>
public class InventoryTooltip : MonoBehaviour
{
    public static InventoryTooltip Instance { get; private set; }

    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;

    [Header("Settings")]
    [Tooltip("Safety distance to prevent the mouse cursor from overlapping the panel.")]
    public Vector2 positionOffset = new Vector2(15f, -15f);

    private Canvas parentCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
        
        parentCanvas = GetComponentInParent<Canvas>();
        HideTooltip();
    }

    private void Update()
    {
        // Follow the mouse position using the New Input System
        if (gameObject.activeSelf && parentCanvas != null && Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 scaledOffset = positionOffset * parentCanvas.scaleFactor;
            transform.position = new Vector3(mousePos.x + scaledOffset.x, mousePos.y + scaledOffset.y, 0f);
        }
    }

    /// <summary> Fills the texts and shows the tooltip. </summary>
    public void ShowTooltip(ItemData item)
    {
        if (parentCanvas != null && Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 scaledOffset = positionOffset * parentCanvas.scaleFactor;
            transform.position = new Vector3(mousePos.x + scaledOffset.x, mousePos.y + scaledOffset.y, 0f);
        }
        
        gameObject.SetActive(true);
        nameText.text = item.displayName;
        descriptionText.text = item.description;
        priceText.text = $"Sell Price: {item.sellPrice}g";
    }

    /// <summary> Hides the tooltip. </summary>
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}