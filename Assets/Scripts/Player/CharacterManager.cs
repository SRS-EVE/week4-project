using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance; // �̱��� ���� �ش������ ĳ���͸Ŵ��� Ŭ������ ������� ���� �ν��Ͻ� *�ν��Ͻ��� = Ŭ������ ������� �Ͽ� ������ ������ ��ü
    public static CharacterManager Instance // �ν��Ͻ� ������Ƽ *������Ƽ��? = �ܺο��� Ŭ���� ������ ������ �����ϰ� �����ϰ� ���ִ� ���
    {
        get // get ������Ƽ�� ���� �������� ������
        {
            if(_instance == null) // �ν��Ͻ��� null�̶��
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>(); // CharacterManager��� �� �̸��� ������Ʈ�� ����� �� ������Ʈ�� ĳ���͸Ŵ��� ��ũ��Ʈ�� Addcomponent�� �߰��ؼ� CharacterManager Ŭ������ �ν��Ͻ��� �����ϰ� _instance�� �Ҵ��Ѵ�.
            }
            return _instance; // �ν��Ͻ��� null�� �ƴ϶�� ������ �ν��Ͻ��� ��ȯ
        }
    }

    public Player _player; // PlayerŸ���� �÷��̾� ���� ����

    public Player Player // �ܺο��� _player�� �����ϴ� ������Ƽ
    {
        get { return _player; } // _Player�� ���� ��ȯ�ϴ� ������Ƽ
        set { _player = value; } // _Player�� ���ο� ���� �����ϴ� ������Ƽ
    }

    private void Awake() // Awake �޼���� ��ũ��Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    {
        if(_instance == null) // �ν��Ͻ��� null�̶��
        {
            _instance = this; // ���� ��ũ��Ʈ�� �ν��Ͻ��� _instance�� �Ҵ�
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �� ������Ʈ�� �ı����� �ʵ��� ����
        }
        else
        {
            if(_instance != this) // �ν��Ͻ��� null�� �ƴ϶��
            {
                Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �� ������Ʈ�� �ı�
            }
        }
    }
}
