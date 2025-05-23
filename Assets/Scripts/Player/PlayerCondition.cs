using System;
using UnityEngine;

// 여기서 플레이어의 컨디션 조절(health, hunger, stamina)

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UiCondition uiCondition; // PlayerCondition에서 가져다 쓰기 위해 참조 변수로 선언
    Condition health { get{ return uiCondition.health; } } // 읽기전용 프로퍼티 선언 get만 사용해 읽어오기만함
    Condition stamina { get { return uiCondition.stamina; } } // 스태미너 상태를 읽어옴

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
                Debug.Log("[PlayerCondition] uiCondition을 강제로 연결했습니다.");
                return;
            }
            Debug.LogWarning("uiCondition이 아직 할당되지 않았습니다.");
            return;
        }

        if(health.curValue <= 0f) // 체력이 0이하가되면
        {
            Die(); // 죽음함수 호출
        }
    }

    public void Heal(float amount) // 힐 함수
    {
        health.Add(amount);
    }

    public void Stamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Die() // 죽음함수
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
