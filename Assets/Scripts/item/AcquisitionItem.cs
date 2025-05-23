using JetBrains.Annotations;
using UnityEngine;

public class AcquisitionItem : MonoBehaviour
{
    public ConsumableType data;
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            switch (data)
            {
                case ConsumableType.stamina:
                    FindObjectOfType<ItemUseInventory>().AddItem(data, 1);
                    Destroy(gameObject);
                    break;
                case ConsumableType.Health:
                    FindObjectOfType<ItemUseInventory>().AddItem(data, 1);
                    Destroy(gameObject);
                    break;
            }
            
        }
    }
}
