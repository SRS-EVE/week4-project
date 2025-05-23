using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] // inspector 창에서 변수들을 보기좋게 정리하기 위한 태그 코드실행에는 아무영향이 없음 [inspector창에 아래 변수들이 묶여서 정리됨]
    public float moveSpeed; // 플레이어의 이동속도
    public float jumpPower; // 점프를 가하는 힘
    private Vector2 curMovementInput; // inputAction에서 받아올 값들을 할당할 변수
    public LayerMask groundLayerMask; // 플레이어가 땅에 붙어있는지 아닌지를 체크하기위한

    [Header("Look")] // inspector 창에서 변수들을 보기좋게 정리하기 위한 태그 코드실행에는 아무영향이 없음 [inspector창에 아래 변수들이 묶여서 정리됨] 
    public Transform camerContainer; // Unity에서 씬 안의 특정 오브젝트(Transform)를 외부에서 연결할 수 있게 만든 변수 선언
    public float minXLook; // 카메라 x축 회전범위의 최소값
    public float maxXLook; // 카메라 x축 회전범위의 최대값
    private float camCurXRot; // 카메라의 현재 X축 회전값을 저장할 변수
                              // 이 값은 마우스의 입력 변화량(delta value)을 누적해서 계산함
                              // delta value = 변화량, 즉 마우스를 얼마나 움직였는지 (예: 마우스 Y를 얼마나 위/아래로 움직였는지)
    public float lookSensitivity; // 카메라 회전 민감도
    private Vector2 mouseDelta; // inputAction에서 전해주는값 : Input System에서 마우스를 얼마만큼 움직였는지에 대한 변화량(delta)
    public bool canLook = true;

    public Action inventory;
    private Rigidbody _rigidbody; // 리지드바디 컴포넌트 호출

    private void Awake() // 스크립트가 실행될때 최초로 실행되는 함수
    {
        _rigidbody = GetComponent<Rigidbody>(); // Rigidbody를 _rigidbody에 할당
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스커서를 숨기는 로직
    }

    // Update is called once per frame
    void FixedUpdate() // 물리연산은 여기서하는게 좋음
    {
        Move();
    }

    private void LateUpdate() // 유니티에서 프레임의 마지막에 호출되는 메서드 Update가 끝난 후에 호출된다
                              // *스크립트의 모든 생명주기를 지나야 프레임이업데이트가 되는데 
                              // 카메라 추적기능을 업데이트에 적게되면 유니티는 업데이트 실행순서를 보장하지 않으므로
                              // 카메라가 먼저 업데이트되어 플레이어를 뒤늦게 따라가는 부작용이 생길 수 있다.
                              // 즉 플레이어 이동연산을 먼저하고 lateUpdate연산을 통해 카메라가 플레이어를 추적하게 해야 자연스럽다
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // transform : 유니티 오브젝트의 transform 컴포넌트(위치, 회전, 크기를 담당)
                                                                                                     // .forward : 오브젝트가 현재 바라보고 있는 정면 방향
                                                                                                     // curMovementInput.y / x : 사용자의 입력값(앞뒤/좌우 방향)
                                                                                                     // * / + 연산자 : 방향 벡터에 입력값을 곱하고 더해 최종 이동 방향을 계산하는 벡터연산
        dir *= moveSpeed; // 이동할때 이동속도를 곱해줌

        dir.y = _rigidbody.velocity.y; // 현재 Rigidbody의 y축 속도를 dir 벡터에 반영(다른행동을 할때 y축속도를 유지하기위한 코드)
                                       // 현재 forward 방향으로 이동하기때문에 x, z축으로만 움직이므로 y가 0으로 고정되어 중력의 영향을 안받음
                                       // 현재 Rigidbody가 가지고 있는 y축 속도 값을따와서 dir.y에 복사한다
                                       // RigidBody.velocity : 리지드바디가 현재 몇 초당 몇 미터로 어느 방향으로 움직이고 있는지를 나타내는 속도 벡터

        _rigidbody.velocity = dir; // 전체이동방향에 속도적용
    }

    void CameraLook() // 카메라 회전을 처리하는 함수 = 축을 기준으로 회전하기때문에 좌우방향은 Y축을 기준으로 상하는 X축을 기준으로 돌린다.
    {
        camCurXRot += mouseDelta.y * lookSensitivity; // Vector값에서 y값 * 민감도 값을 누적해서 camCurXRot에 저장 (상하 회전 누적)
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // camCurXRot 값이 minXLook ~ maxXLook 범위를 넘지 않도록 제한 (고개 꺾임 방지)
        camerContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 카메라 컨테이너의 X축을 회전시켜 상하 시야 구현
                                                                          // 마우스를 아래로 내리면 카메라가 위를 바라보도록 하기 위해 -camCurXRot로 부호 반전
                                                                          // localEulerAngles는 부모(플레이어) 기준으로 회전함
                                                                          // 카메라는 플레이어의 자식 오브젝트이므로 로컬기준으로 회전

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 플레이어 오브젝트 자체를 Y축으로 회전시켜 좌우 시야 구현
                                                                                    // 마우스 X축 입력값에 민감도를 곱해 Y축 회전을 누적
                                                                                    // eulerAngles는 월드 좌표 기준 회전값이므로, 플레이어 전체 방향이 바뀜
                                                                                    // 플레이어는 움직임이 구현되어있으므로 월드좌표를 기준으로 좌우 회전을 해야함
                                                                                    // 만약 플레이어 좌우이야를 로컬 기준으로 하면 캐릭터가 허리를 뒤로꺽고 좌우를 바라보는 듯한 모습이 연출됨
    }

    public void OnMove(InputAction.CallbackContext context) // 플레이어 이동함수
                                                            // InputAction.CallbackContext context = 유니티의 입력 시스템에서
                                                            // 입력 이벤트가 발생했을 때 전달되는 정보를 담는 매개변수
    {
        Debug.Log($"Move context: {context.phase}, value: {context.ReadValue<Vector2>()}");


        if (context.phase == InputActionPhase.Performed) // context의 상태가 Performed인 상태(입력이 성공적으로 수행된 상태 ex : 키를 누르고 있는중 or 눌렀을 때 )
        // if (context.phase == InputActionPhase.Started) // context의 상태가 started인 상태(입력이 막 시작된 상태 = 버튼을 누른 순간)
        {
            curMovementInput = context.ReadValue<Vector2>(); // 입력(context)에서 전달된 값을 Vector2 형식으로 읽어온다
                                                             // ex) (1, 0) 오른쪽 (-1, 0) 왼쪽 (0 , 1) 위 (0, -1) 아래 (0.7, 0.7) 오른쪽 위
        }
        else if (context.phase == InputActionPhase.Canceled) // context의 상태가 canceled인 상태 (키 입력이 끝났을때)
        {
            curMovementInput = Vector2.zero; // (0, 0)
        }
    }
     public void OnLook(InputAction.CallbackContext context)// 카메라 이동함수
                                                            // InputAction.CallbackContext context = 유니티의 입력 시스템에서
                                                            // 입력 이벤트가 발생했을 때 전달되는 정보를 담는 매개변수
    {
        mouseDelta = context.ReadValue<Vector2>(); // mouseDelta변수에 InputAction.CallbackContext 구조체인 context로부터 Vector2형태의 입력값을 할당해줌 = 마우스가 얼마나 움직였는지
                                                   // 보통 마우스 이동이나 조이스틱 이동처럼 2차원 방향값을 읽을 때 사용됨
                                                   // ReadValue<>() 입력된값을 <>안에 값을 형태로 읽는 메서드
                                                   // 이동과다르게 마우스는 값이 계속 유지되므로 값만 읽어오면된다        
    }

    public void OnJump(InputAction.CallbackContext context)// 캐릭터 점프함수 : 해당함수는 inputsystem에 함수로 등록되고 매순간 update처리 할 필요가없음
                                                           // InputAction.CallbackContext context = 유니티의 입력 시스템에서
                                                           // 입력 이벤트가 발생했을 때 전달되는 정보를 담는 매개변수
    {
        if (context.phase == InputActionPhase.Started && isGrounded()) // context의 상태가 started인 상태(입력이 막 시작된 상태 = 버튼을 누른 순간) && 땅에 닿아있을때
        { 
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // rigidbody에 힘을 가하는 함수
                                                                    // Vector2.up = y축방향으로 1만큼의 힘을 가함
                                                                    // ForceMode.Impulse = 순간적인 힘을 구현할때 사용
                                                                    // AddForce(이동방향, ForceMode) 로 사용 기본적으로 ForceMode를 쓰지않으면 Force상태
                                                                    // Force, Impulse, VelocityChange(마찰, 질량을 무시하고 속도변경) 가있음
        }
    }

    bool isGrounded() // 플레이어가 땅에 붙어있는지 아닌지를 체크하는 함수
    {
        Ray[] rays = new Ray[4] // Ray는 레이의 시작위치, 레이의 진행방향을 인자로받음 ex)public Ray(Vector3 origin, Vector3 direction);
        {
            new Ray(transform.position + (transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down), // 현오브젝트의 0.2만큼 앞으로이동, 오브젝트의 위방향으로 0.01만큼이동한 곳에서 아래로 쏘는레이
            new Ray(transform.position + (-transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + transform.up * 0.01f, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) // Physics.Raycast 레이를 쏴서 충돌체를 감지할 때 사용하는 함수
                                                                 // (시작점과 방향을 포함한 레이객체, 레이를 얼마나 멀리 쏠것인지, 어떤레이어에 반응 할 것인지)
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}

// 벡터연산
/*
    1.벡터의 정의

    2차원 벡터 예:
    A = (2, 3)
    B = (5, 1)

    3차원 벡터 예:
    A = (2, 3, 4)
    B = (1, -2, 0)
*/

/*
    2. 벡터 덧셈

    공식:
    A + B = (A₁ + B₁, A₂ + B₂[, A₃ + B₃])

    예시 (2D):
    (2, 3) + (5, 1) = (2 + 5, 3 + 1) = (7, 4)

    예시 (3D):
    (2, 3, 4) + (1, -2, 0) = (2+1, 3+(-2), 4+0) = (3, 1, 4)
 */

/*
    3. 벡터 뺄셈

    공식:
    A - B = (A₁ - B₁, A₂ - B₂[, A₃ - B₃])

    예시 (2D):
    (2, 3) - (5, 1) = (2 - 5, 3 - 1) = (-3, 2)
    
    예시 (3D):
    (2, 3, 4) - (1, -2, 0) = (2-1, 3-(-2), 4-0) = (1, 5, 4)
 */

/*
    4. 스칼라 곱 (벡터 × 숫자)

    공식:
    kA = (kA₁, kA₂[, kA₃])

    예시:
    2 × (2, 3) = (4, 6)
    -3 × (1, -2, 0) = (-3, 6, 0)
 */


