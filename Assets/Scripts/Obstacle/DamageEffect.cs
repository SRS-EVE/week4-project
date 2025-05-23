using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageEffect : MonoBehaviour, IObstacleEffect
{
    public int damage;
    public float damageRate;
    public float effectDuration;

    private bool isActive = false; // 중복방지

    public void ApplyEffect(GameObject target)
    {
        if (isActive) return;

        isActive = true;
        StartCoroutine(DamageOvertime(target));
    }
    
    private IEnumerator DamageOvertime(GameObject target)
    {
        float time = 0f;

        while(time < effectDuration)
        {
            if(target.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakePhysicalDamage(damage);
            }
            yield return new WaitForSeconds(damageRate);
            time += damageRate;
        }
        isActive = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 playerFootPos = other.bounds.center - new Vector3(0, other.bounds.extents.y, 0);
        float heightDiff = playerFootPos.y - transform.position.y;

        if (heightDiff > 0.05f)
        {
            ApplyEffect(other.gameObject);
        }
    }
}
