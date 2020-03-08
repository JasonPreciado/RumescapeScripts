using UnityEngine;

public class InventoryUI : MonoBehaviour
{
   // Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        //inventory = (Inventory)Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
      // inventory.onItemChangedCallback += UpdateUI;
    }

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");
    }
}
