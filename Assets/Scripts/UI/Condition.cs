using UnityEngine;
using UnityEngine.UI;

// ����ǹ�

public class Condition : MonoBehaviour
{
    public float curValue; // ������� ���¸� ��Ÿ���� ����
    public float startValue; // ���۽� ���¸� ��Ÿ���� ����
    public float maxValue; // ������� �ִ밪�� ��Ÿ���� ����
    public float passiveValue; // hunger, stamina�� �����ϰ� ���ϴ� ������ ��ȭ�� ��Ÿ���� ����
    public Image uiBar; // �̹������ִ� fillamount�� ����ϱ����� �̹��� ������Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue; // �����Ҷ��� startValue
    }

    // Update is called once per frame
    void Update()
    {
        // ui������Ʈ
        uiBar.fillAmount = GetPercentage();
    }
    float GetPercentage() // ui�� fillamount ǥ���� ���� ���� ��ȯ�ϴ� �Լ�
    {
        return curValue / maxValue; // 0~1 ���̰������� �پ�ų� �þ�Ƿ� curValue / maxValue���� �ʿ���
    }
    public void Add(float value) // ����� ���¹ٸ� ������Ű�� �Լ�
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }
    public void Subtract(float value) // ����� ���¹ٸ� ���ҽ�Ű�� �Լ�
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
