using UnityEngine;
using UnityEngine.UI;

// 컨디션바

public class Condition : MonoBehaviour
{
    public float curValue; // 현재바의 상태를 나타내는 변수
    public float startValue; // 시작시 상태를 나타내는 변수
    public float maxValue; // 어떤상태의 최대값을 나타내는 변수
    public float passiveValue; // hunger, stamina등 꾸준하게 변하는 값들의 변화를 나타내는 변수
    public Image uiBar; // 이미지에있는 fillamount를 사용하기위해 이미지 컴포넌트 선언

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue; // 시작할때는 startValue
    }

    // Update is called once per frame
    void Update()
    {
        // ui업데이트
        uiBar.fillAmount = GetPercentage();
    }
    float GetPercentage() // ui의 fillamount 표현을 위한 값을 반환하는 함수
    {
        return curValue / maxValue; // 0~1 사이값에따라 줄어거나 늘어나므로 curValue / maxValue값이 필요함
    }
    public void Add(float value) // 컨디션 상태바를 증가시키는 함수
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }
    public void Subtract(float value) // 컨디션 상태바를 감소시키는 함수
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
