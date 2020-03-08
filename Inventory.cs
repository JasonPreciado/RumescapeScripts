using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public const int slots = 4;
    public Image[] itemImages = new Image[slots];
    public Item[] items = new Item[slots];

    public void AddItem(Item currentItem)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = currentItem;
                itemImages[i].sprite = currentItem.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(Item currentItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == currentItem)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }
}
