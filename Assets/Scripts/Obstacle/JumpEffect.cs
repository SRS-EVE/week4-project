using UnityEngine;

public class JumpEffect : MonoBehaviour, IObstacleEffect
{
    public float jumpPower = 15f;

    public void ApplyEffect(GameObject target)
    {
        Debug.Log("�����Ѿ��");
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // �÷��̾ �����뺸�� ���� �־�� �ߵ���

        Vector3 playerFootPos = other.bounds.center - new Vector3(0, other.bounds.extents.y, 0);
        float heightDiff = playerFootPos.y - transform.position.y;

        if (heightDiff > 0.05f)
        {
            //Debug.Log("���� ������ ���� �� ���� �ߵ�");
            ApplyEffect(other.gameObject);
        }
        else
        {
            //Debug.Log("���� �Ǵ� �Ʒ����� �浹 �� ����");
        }

    }
}
