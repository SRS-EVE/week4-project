using TMPro;
using UnityEngine;

public class ItemUseInventory : MonoBehaviour
{
    public TextMeshProUGUI[] countTexts; // 슬롯 수량 텍스트 (길이 4)
    public ConsumableType[] itemTypes = new ConsumableType[3]; // 슬롯 아이템 종류
    private int[] itemCounts = new int[4]; // 슬롯별 수량

    private PlayerCondition condition;

    public float healValue = 20f;
    public float staminaValue = 10f;

    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseItem(3);
    }

    public void AddItem(ConsumableType type, int amount)
    {
        for (int i = 0; i < itemTypes.Length; i++)
        {
            if (itemTypes[i] == type)
            {
                itemCounts[i] += amount;
                UpdateUI();
                return;
            }
        }

        Debug.LogWarning("아이템 타입이 어떤 슬롯에도 지정되지 않았습니다.");
    }

    public void UseItem(int index)
    {
        if (itemCounts[index] <= 0) return;

        Debug.Log("Using item: " + itemTypes[index]);

        switch (itemTypes[index])
        {
            case ConsumableType.stamina:
                condition.Stamina(healValue);
                break;
            case ConsumableType.Health:
                condition.Heal(staminaValue);
                break;
        }

        itemCounts[index]--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < countTexts.Length; i++)
        {
            countTexts[i].text = itemCounts[i].ToString();
        }
    }
}
