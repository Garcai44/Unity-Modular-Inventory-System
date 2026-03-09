# 🎒 Modular & Decoupled Inventory System for Unity

![Inventory System Demo](InventorySystem.gif)

A robust, production-ready inventory system for Unity. Designed with a clean architecture that strictly separates data, logic, and user interface.

## ✨ Key Features

* **100% Decoupled Architecture:** The backend logic (`InventoryManager`) communicates with the frontend (`InventoryUI`) strictly through UnityEvents. You can delete or completely change the UI without breaking a single line of inventory logic.
* **ScriptableObject Driven:** Items are created as data containers (`ItemData`), making it extremely easy for game designers to add hundreds of items (weapons, consumables, materials) without touching code.
* **Smart Stacking Logic:** Automatically handles item stacking, limits (`maxStackSize`), and remaining amounts across multiple slots.
* **Resolution-Independent Tooltips:** Includes a dynamic tooltip system that strictly follows the mouse cursor, utilizing Canvas scale factors to prevent overlapping or flickering at any resolution (4K, 1080p, or mobile).
* **Modern Input Ready:** Fully migrated to Unity's New Input System for both hotkeys and UI interactions (Left click to consume, Right click to drop).

## 🗂️ Architecture & Folder Structure

To ensure maximum scalability, the scripts are strictly categorized:
* `/Data`: Contains the `ItemData` blueprints and the `InventorySlot` classes.
* `/Logic`: Houses the core mathematical engine (`InventoryManager`).
* `/UI`: Contains the visual representation scripts (`InventorySlotUI`, `InventoryUI`, and `InventoryTooltip`).

## 🚀 How to Use (Plug & Play)

1. Import the folder into your Unity project.
2. Open the included `InventorySystemScene` to see the complete setup.
3. Right-click in the Project window: `Create > ModularInventory > New Item` to create your own items.
4. Drag your new `ItemData` into the `PickupTester` script to start adding them to the backpack!

---
👨‍💻 **About the Developer**
Hi! I'm Gonzalo, a Game Development student passionate about writing clean, modular, and easy-to-use code for Unity. 

Looking for modular assets for your game? I am available for freelance work! I focus on creating specific, well-structured micro-systems (like this inventory or combat setups) to save indie developers time. Let's talk!
