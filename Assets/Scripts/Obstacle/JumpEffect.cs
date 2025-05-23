using UnityEngine;

public class JumpEffect : MonoBehaviour, IObstacleEffect
{
    public float jumpPower = 15f;

    public void ApplyEffect(GameObject target)
    {
        Debug.Log("정보넘어옴");
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 플레이어가 점프대보다 위에 있어야 발동됨

        Vector3 playerFootPos = other.bounds.center - new Vector3(0, other.bounds.extents.y, 0);
        float heightDiff = playerFootPos.y - transform.position.y;

        if (heightDiff > 0.05f)
        {
            //Debug.Log("정상 위에서 진입 → 점프 발동");
            ApplyEffect(other.gameObject);
        }
        else
        {
            //Debug.Log("옆면 또는 아래에서 충돌 → 무시");
        }

    }
}
