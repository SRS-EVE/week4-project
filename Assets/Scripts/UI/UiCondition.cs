using UnityEngine;

// conditions �ڽĿ�����Ʈ���� �����ϴ� ��ũ��Ʈ

public class UiCondition : MonoBehaviour
{
    public Condition health; // ü��
    public Condition stamina; // ���¹̳�
                              // Ŭ������ ������Ÿ������ �۵��ϹǷ� ���𰡴�


    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this; // CharacterManager�� �ν��Ͻ� �Ҵ�Ǿ� �ִ� Player��ũ��Ʈ ���ο�
                                                                       // ������ Condition ������Ʈ���� �����ϴ� PlayerConditon ���ο� ����������
                                                                       // ����Ǿ��ִ� uiCondition�� UiCondition ��ũ��Ʈ�� �Ҵ�
    }

}
