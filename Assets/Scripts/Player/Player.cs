using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller; // 플레이어컨트롤러를 참조하는 변수
    public PlayerCondition condition; // 플레이어컨디션을 참조하는 변수

    public ItemData itemData;
    public Action addItem;

    public ObstacleData obsData;
    public Action obsInfo;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; // CharacterManager의 인스턴스에 현재 플레이어를 할당
                                                 // CharacterManger.Instance = 싱글턴 인스턴스를 가져옴
                                                 // .Player = this = 프로퍼티의 set이 호출
                                                 // set 내부에서 _player = this 수행                                                    
        controller = GetComponent<PlayerController>(); // 플레이어컨트롤러 컴포넌트를 가져와서 controller 변수에 할당
        condition = GetComponent<PlayerCondition>(); // 플레이어컨디션 컴포넌트를 가져와서 condition 변수에 할당
    }
}
