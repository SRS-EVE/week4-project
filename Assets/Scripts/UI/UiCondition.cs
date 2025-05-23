using UnityEngine;

// conditions 자식오브젝트들을 관리하는 스크립트

public class UiCondition : MonoBehaviour
{
    public Condition health; // 체력
    public Condition stamina; // 스태미너
                              // 클래스는 데이터타입으로 작동하므로 선언가능


    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this; // CharacterManager에 인스턴스 할당되어 있는 Player스크립트 내부에
                                                                       // 참조된 Condition 컴포넌트에서 참조하는 PlayerConditon 내부에 참조변수로
                                                                       // 선언되어있는 uiCondition에 UiCondition 스크립트를 할당
    }

}
