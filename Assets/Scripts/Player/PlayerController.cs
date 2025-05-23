using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] // inspector â���� �������� �������� �����ϱ� ���� �±� �ڵ���࿡�� �ƹ������� ���� [inspectorâ�� �Ʒ� �������� ������ ������]
    public float moveSpeed; // �÷��̾��� �̵��ӵ�
    public float jumpPower; // ������ ���ϴ� ��
    private Vector2 curMovementInput; // inputAction���� �޾ƿ� ������ �Ҵ��� ����
    public LayerMask groundLayerMask; // �÷��̾ ���� �پ��ִ��� �ƴ����� üũ�ϱ�����

    [Header("Look")] // inspector â���� �������� �������� �����ϱ� ���� �±� �ڵ���࿡�� �ƹ������� ���� [inspectorâ�� �Ʒ� �������� ������ ������] 
    public Transform camerContainer; // Unity���� �� ���� Ư�� ������Ʈ(Transform)�� �ܺο��� ������ �� �ְ� ���� ���� ����
    public float minXLook; // ī�޶� x�� ȸ�������� �ּҰ�
    public float maxXLook; // ī�޶� x�� ȸ�������� �ִ밪
    private float camCurXRot; // ī�޶��� ���� X�� ȸ������ ������ ����
                              // �� ���� ���콺�� �Է� ��ȭ��(delta value)�� �����ؼ� �����
                              // delta value = ��ȭ��, �� ���콺�� �󸶳� ���������� (��: ���콺 Y�� �󸶳� ��/�Ʒ��� ����������)
    public float lookSensitivity; // ī�޶� ȸ�� �ΰ���
    private Vector2 mouseDelta; // inputAction���� �����ִ°� : Input System���� ���콺�� �󸶸�ŭ ������������ ���� ��ȭ��(delta)
    public bool canLook = true;

    public Action inventory;
    private Rigidbody _rigidbody; // ������ٵ� ������Ʈ ȣ��

    private void Awake() // ��ũ��Ʈ�� ����ɶ� ���ʷ� ����Ǵ� �Լ�
    {
        _rigidbody = GetComponent<Rigidbody>(); // Rigidbody�� _rigidbody�� �Ҵ�
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺Ŀ���� ����� ����
    }

    // Update is called once per frame
    void FixedUpdate() // ���������� ���⼭�ϴ°� ����
    {
        Move();
    }

    private void LateUpdate() // ����Ƽ���� �������� �������� ȣ��Ǵ� �޼��� Update�� ���� �Ŀ� ȣ��ȴ�
                              // *��ũ��Ʈ�� ��� �����ֱ⸦ ������ �������̾�����Ʈ�� �Ǵµ� 
                              // ī�޶� ��������� ������Ʈ�� ���ԵǸ� ����Ƽ�� ������Ʈ ��������� �������� �����Ƿ�
                              // ī�޶� ���� ������Ʈ�Ǿ� �÷��̾ �ڴʰ� ���󰡴� ���ۿ��� ���� �� �ִ�.
                              // �� �÷��̾� �̵������� �����ϰ� lateUpdate������ ���� ī�޶� �÷��̾ �����ϰ� �ؾ� �ڿ�������
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // transform : ����Ƽ ������Ʈ�� transform ������Ʈ(��ġ, ȸ��, ũ�⸦ ���)
                                                                                                     // .forward : ������Ʈ�� ���� �ٶ󺸰� �ִ� ���� ����
                                                                                                     // curMovementInput.y / x : ������� �Է°�(�յ�/�¿� ����)
                                                                                                     // * / + ������ : ���� ���Ϳ� �Է°��� ���ϰ� ���� ���� �̵� ������ ����ϴ� ���Ϳ���
        dir *= moveSpeed; // �̵��Ҷ� �̵��ӵ��� ������

        dir.y = _rigidbody.velocity.y; // ���� Rigidbody�� y�� �ӵ��� dir ���Ϳ� �ݿ�(�ٸ��ൿ�� �Ҷ� y��ӵ��� �����ϱ����� �ڵ�)
                                       // ���� forward �������� �̵��ϱ⶧���� x, z�����θ� �����̹Ƿ� y�� 0���� �����Ǿ� �߷��� ������ �ȹ���
                                       // ���� Rigidbody�� ������ �ִ� y�� �ӵ� �������ͼ� dir.y�� �����Ѵ�
                                       // RigidBody.velocity : ������ٵ� ���� �� �ʴ� �� ���ͷ� ��� �������� �����̰� �ִ����� ��Ÿ���� �ӵ� ����

        _rigidbody.velocity = dir; // ��ü�̵����⿡ �ӵ�����
    }

    void CameraLook() // ī�޶� ȸ���� ó���ϴ� �Լ� = ���� �������� ȸ���ϱ⶧���� �¿������ Y���� �������� ���ϴ� X���� �������� ������.
    {
        camCurXRot += mouseDelta.y * lookSensitivity; // Vector������ y�� * �ΰ��� ���� �����ؼ� camCurXRot�� ���� (���� ȸ�� ����)
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // camCurXRot ���� minXLook ~ maxXLook ������ ���� �ʵ��� ���� (�� ���� ����)
        camerContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ī�޶� �����̳��� X���� ȸ������ ���� �þ� ����
                                                                          // ���콺�� �Ʒ��� ������ ī�޶� ���� �ٶ󺸵��� �ϱ� ���� -camCurXRot�� ��ȣ ����
                                                                          // localEulerAngles�� �θ�(�÷��̾�) �������� ȸ����
                                                                          // ī�޶�� �÷��̾��� �ڽ� ������Ʈ�̹Ƿ� ���ñ������� ȸ��

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // �÷��̾� ������Ʈ ��ü�� Y������ ȸ������ �¿� �þ� ����
                                                                                    // ���콺 X�� �Է°��� �ΰ����� ���� Y�� ȸ���� ����
                                                                                    // eulerAngles�� ���� ��ǥ ���� ȸ�����̹Ƿ�, �÷��̾� ��ü ������ �ٲ�
                                                                                    // �÷��̾�� �������� �����Ǿ������Ƿ� ������ǥ�� �������� �¿� ȸ���� �ؾ���
                                                                                    // ���� �÷��̾� �¿��̾߸� ���� �������� �ϸ� ĳ���Ͱ� �㸮�� �ڷβ��� �¿츦 �ٶ󺸴� ���� ����� �����
    }

    public void OnMove(InputAction.CallbackContext context) // �÷��̾� �̵��Լ�
                                                            // InputAction.CallbackContext context = ����Ƽ�� �Է� �ý��ۿ���
                                                            // �Է� �̺�Ʈ�� �߻����� �� ���޵Ǵ� ������ ��� �Ű�����
    {
        Debug.Log($"Move context: {context.phase}, value: {context.ReadValue<Vector2>()}");


        if (context.phase == InputActionPhase.Performed) // context�� ���°� Performed�� ����(�Է��� ���������� ����� ���� ex : Ű�� ������ �ִ��� or ������ �� )
        // if (context.phase == InputActionPhase.Started) // context�� ���°� started�� ����(�Է��� �� ���۵� ���� = ��ư�� ���� ����)
        {
            curMovementInput = context.ReadValue<Vector2>(); // �Է�(context)���� ���޵� ���� Vector2 �������� �о�´�
                                                             // ex) (1, 0) ������ (-1, 0) ���� (0 , 1) �� (0, -1) �Ʒ� (0.7, 0.7) ������ ��
        }
        else if (context.phase == InputActionPhase.Canceled) // context�� ���°� canceled�� ���� (Ű �Է��� ��������)
        {
            curMovementInput = Vector2.zero; // (0, 0)
        }
    }
     public void OnLook(InputAction.CallbackContext context)// ī�޶� �̵��Լ�
                                                            // InputAction.CallbackContext context = ����Ƽ�� �Է� �ý��ۿ���
                                                            // �Է� �̺�Ʈ�� �߻����� �� ���޵Ǵ� ������ ��� �Ű�����
    {
        mouseDelta = context.ReadValue<Vector2>(); // mouseDelta������ InputAction.CallbackContext ����ü�� context�κ��� Vector2������ �Է°��� �Ҵ����� = ���콺�� �󸶳� ����������
                                                   // ���� ���콺 �̵��̳� ���̽�ƽ �̵�ó�� 2���� ���Ⱚ�� ���� �� ����
                                                   // ReadValue<>() �ԷµȰ��� <>�ȿ� ���� ���·� �д� �޼���
                                                   // �̵����ٸ��� ���콺�� ���� ��� �����ǹǷ� ���� �о����ȴ�        
    }

    public void OnJump(InputAction.CallbackContext context)// ĳ���� �����Լ� : �ش��Լ��� inputsystem�� �Լ��� ��ϵǰ� �ż��� updateó�� �� �ʿ䰡����
                                                           // InputAction.CallbackContext context = ����Ƽ�� �Է� �ý��ۿ���
                                                           // �Է� �̺�Ʈ�� �߻����� �� ���޵Ǵ� ������ ��� �Ű�����
    {
        if (context.phase == InputActionPhase.Started && isGrounded()) // context�� ���°� started�� ����(�Է��� �� ���۵� ���� = ��ư�� ���� ����) && ���� ���������
        { 
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // rigidbody�� ���� ���ϴ� �Լ�
                                                                    // Vector2.up = y��������� 1��ŭ�� ���� ����
                                                                    // ForceMode.Impulse = �������� ���� �����Ҷ� ���
                                                                    // AddForce(�̵�����, ForceMode) �� ��� �⺻������ ForceMode�� ���������� Force����
                                                                    // Force, Impulse, VelocityChange(����, ������ �����ϰ� �ӵ�����) ������
        }
    }

    bool isGrounded() // �÷��̾ ���� �پ��ִ��� �ƴ����� üũ�ϴ� �Լ�
    {
        Ray[] rays = new Ray[4] // Ray�� ������ ������ġ, ������ ��������� ���ڷι��� ex)public Ray(Vector3 origin, Vector3 direction);
        {
            new Ray(transform.position + (transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down), // ��������Ʈ�� 0.2��ŭ �������̵�, ������Ʈ�� ���������� 0.01��ŭ�̵��� ������ �Ʒ��� ��·���
            new Ray(transform.position + (-transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + transform.up * 0.01f, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) // Physics.Raycast ���̸� ���� �浹ü�� ������ �� ����ϴ� �Լ�
                                                                 // (�������� ������ ������ ���̰�ü, ���̸� �󸶳� �ָ� �������, ����̾ ���� �� ������)
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

// ���Ϳ���
/*
    1.������ ����

    2���� ���� ��:
    A = (2, 3)
    B = (5, 1)

    3���� ���� ��:
    A = (2, 3, 4)
    B = (1, -2, 0)
*/

/*
    2. ���� ����

    ����:
    A + B = (A�� + B��, A�� + B��[, A�� + B��])

    ���� (2D):
    (2, 3) + (5, 1) = (2 + 5, 3 + 1) = (7, 4)

    ���� (3D):
    (2, 3, 4) + (1, -2, 0) = (2+1, 3+(-2), 4+0) = (3, 1, 4)
 */

/*
    3. ���� ����

    ����:
    A - B = (A�� - B��, A�� - B��[, A�� - B��])

    ���� (2D):
    (2, 3) - (5, 1) = (2 - 5, 3 - 1) = (-3, 2)
    
    ���� (3D):
    (2, 3, 4) - (1, -2, 0) = (2-1, 3-(-2), 4-0) = (1, 5, 4)
 */

/*
    4. ��Į�� �� (���� �� ����)

    ����:
    kA = (kA��, kA��[, kA��])

    ����:
    2 �� (2, 3) = (4, 6)
    -3 �� (1, -2, 0) = (-3, 6, 0)
 */


