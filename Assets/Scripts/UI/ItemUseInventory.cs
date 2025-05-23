using TMPro;
using UnityEngine;

public class ItemUseInventory : MonoBehaviour
{
    public TextMeshProUGUI[] countTexts; // 슬롯 수량 텍스트 (길이 4)
    public ConsumableType[] itemTypes = new ConsumableType[4]; // 슬롯 아이템 종류
    private int[] itemCounts = new int[4]; // 슬롯별 수량

    private PlayerCondition condition;

    public float healValue = 20f;
    public float staminaValue = 10f;

    private void Start()
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
        Debug.Log($"[AddItem] 호출됨 - {type}");

    for (int i = 0; i < itemTypes.Length; i++)
    {
        Debug.Log($"[AddItem] 슬롯 {i} = {itemTypes[i]} vs {type}");

        if (itemTypes[i] == type)
        {
            itemCounts[i] += amount;
            Debug.Log($"[AddItem] {type} 아이템 추가됨, 현재 개수: {itemCounts[i]}");
            UpdateUI();
            return;
        }
    }

    Debug.LogWarning($"[AddItem] {type} 을 itemTypes에서 찾지 못함!");
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
            case ConsumableType.buff:
                BuffManager.Instance.ApplySpeedBuff();
                break;
        }

        itemCounts[index]--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < countTexts.Length && i < itemCounts.Length; i++)
        {
            Debug.Log($"[UpdateUI] 슬롯 {i} 텍스트 = {itemCounts[i]}");
            if (countTexts[i] != null)
                countTexts[i].text = itemCounts[i].ToString();
            else
                Debug.LogWarning($"[UpdateUI] countTexts[{i}] 가 null입니다!");
        }
    }

}
