using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance; // 싱글턴 패턴 해당로직은 캐릭터매니저 클래스를 기반으로 만든 인스턴스 *인스턴스란 = 클래스를 기반으로 하여 실제로 생성된 객체
    public static CharacterManager Instance // 인스턴스 프로퍼티 *프로퍼티란? = 외부에서 클래스 내부의 변수에 안전하게 접근하게 해주는 방법
    {
        get // get 프로퍼티는 값을 가져오는 접근자
        {
            if(_instance == null) // 인스턴스가 null이라면
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>(); // CharacterManager라는 새 이름의 오브젝트를 만들고 그 오브젝트에 캐릭터매니저 스크립트를 Addcomponent로 추가해서 CharacterManager 클래스의 인스턴스를 생성하고 _instance에 할당한다.
            }
            return _instance; // 인스턴스가 null이 아니라면 기존의 인스턴스를 반환
        }
    }

    public Player _player; // Player타입의 플레이어 변수 선언

    public Player Player // 외부에서 _player에 접근하는 프로퍼티
    {
        get { return _player; } // _Player에 값을 반환하는 프로퍼티
        set { _player = value; } // _Player에 새로운 값을 설정하는 프로퍼티
    }

    private void Awake() // Awake 메서드는 스크립트가 활성화될 때 호출되는 메서드
    {
        if(_instance == null) // 인스턴스가 null이라면
        {
            _instance = this; // 현재 스크립트의 인스턴스를 _instance에 할당
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 이 오브젝트는 파괴되지 않도록 설정
        }
        else
        {
            if(_instance != this) // 인스턴스가 null이 아니라면
            {
                Destroy(gameObject); // 이미 인스턴스가 존재하면 이 오브젝트를 파괴
            }
        }
    }
}
