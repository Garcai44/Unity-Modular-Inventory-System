using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simple tester class to add items using keyboard inputs (New Input System).
/// </summary>
public class PickupTester : MonoBehaviour
{
    [Header("References")]
    public InventoryManager inventory;
    public ItemData healthPotion;
    public ItemData ironSword;

    private void Update()
    {
        // Ensure the keyboard is connected
        if (Keyboard.current == null) return;

        // Press 'P' to add Potions
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            inventory.AddItem(healthPotion, 50);
            Debug.Log("Added 5 Potions!");
        }
        
        // Press 'S' to add a Sword
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            inventory.AddItem(ironSword, 1);
            Debug.Log("Added 1 Sword!");
        }
    }
}