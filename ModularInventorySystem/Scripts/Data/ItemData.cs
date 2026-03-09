using UnityEngine;

/// <summary>
/// Categories to classify items. Useful for sorting or filtering in the UI.
/// </summary>
public enum ItemType { Consumable, Weapon, Equipment, Material, Quest }

/// <summary>
/// Blueprint for any item in the game. 
/// Create new items via Right-Click -> ModularInventory -> New Item.
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "ModularInventory/New Item", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string id = "item_001"; // Unique identifier useful for saving/loading game states
    public string displayName = "New Item";
    [TextArea(2, 4)]
    public string description = "Item description goes here.";
    public Sprite icon;

    [Header("Categorization")]
    public ItemType itemType = ItemType.Material;

    [Header("Stacking Rules")]
    [Tooltip("Can the player hold multiple of this item in a single slot?")]
    public bool isStackable = true;
    
    [Tooltip("Maximum amount of this item per slot (Ignored if not stackable).")]
    public int maxStackSize = 99;

    [Header("Economy")]
    public int sellPrice = 10;
    public int buyPrice = 20;
}