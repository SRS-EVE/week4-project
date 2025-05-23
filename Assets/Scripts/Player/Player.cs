using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller; // �÷��̾���Ʈ�ѷ��� �����ϴ� ����
    public PlayerCondition condition; // �÷��̾�������� �����ϴ� ����

    public ItemData itemData;
    public Action addItem;

    public ObstacleData obsData;
    public Action obsInfo;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; // CharacterManager�� �ν��Ͻ��� ���� �÷��̾ �Ҵ�
                                                 // CharacterManger.Instance = �̱��� �ν��Ͻ��� ������
                                                 // .Player = this = ������Ƽ�� set�� ȣ��
                                                 // set ���ο��� _player = this ����                                                    
        controller = GetComponent<PlayerController>(); // �÷��̾���Ʈ�ѷ� ������Ʈ�� �����ͼ� controller ������ �Ҵ�
        condition = GetComponent<PlayerCondition>(); // �÷��̾������ ������Ʈ�� �����ͼ� condition ������ �Ҵ�
    }
}
