using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public int price;
    public Button buyButton;
    public GameObject itemPrefab;
}

public class ShopManager : MonoBehaviour
{
    public Text monedasText;
    public ShopItem[] shopItems;

    void Start()
    {
        UpdateMonedasUI();
        foreach(var item in shopItems)
            item.buyButton.onClick.AddListener(()=> BuyItem(item));
    }

    public void UpdateMonedasUI() => monedasText.text = $"Monedas: {PlayerPrefs.GetInt("Monedas",0)}";

    void BuyItem(ShopItem item)
    {
        int monedas = PlayerPrefs.GetInt("Monedas",0);
        if(monedas>=item.price)
        {
            monedas -= item.price;
            PlayerPrefs.SetInt("Monedas", monedas);
            PlayerPrefs.SetInt(item.itemName, 1);
            UpdateMonedasUI();
            if(item.itemPrefab != null) ApplySkin(item.itemPrefab);
            item.buyButton.interactable = false;
        }
        else Debug.Log("No tienes suficientes monedas");
    }

    void ApplySkin(GameObject skinPrefab)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach(Transform child in player.transform) child.gameObject.SetActive(false);
        Instantiate(skinPrefab, player.transform);
    }

    public void CloseShop() => gameObject.SetActive(false);
}
