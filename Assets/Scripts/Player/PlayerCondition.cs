using System;
using UnityEngine;

// ���⼭ �÷��̾��� ����� ����(health, hunger, stamina)

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UiCondition uiCondition; // PlayerCondition���� ������ ���� ���� ���� ������ ����
    Condition health { get{ return uiCondition.health; } } // �б����� ������Ƽ ���� get�� ����� �о���⸸��
    Condition stamina { get { return uiCondition.stamina; } } // ���¹̳� ���¸� �о��

    public event Action onTakeDamage;

    
    // Update is called once per frame


    void Update()
    {
        if (uiCondition == null)
        {
            UiCondition found = FindObjectOfType<UiCondition>();
            if (found != null)
            {
                uiCondition = found;
                Debug.Log("[PlayerCondition] uiCondition�� ������ �����߽��ϴ�.");
                return;
            }
            Debug.LogWarning("uiCondition�� ���� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if(health.curValue <= 0f) // ü���� 0���ϰ��Ǹ�
        {
            Die(); // �����Լ� ȣ��
        }
    }

    public void Heal(float amount) // �� �Լ�
    {
        health.Add(amount);
    }

    public void Stamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Die() // �����Լ�
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
