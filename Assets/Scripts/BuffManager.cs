using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    public PlayerController controller;

    public float buffValue = 2f;
    public float duration = 5f;

    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;
    }

    public void ApplySpeedBuff()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(SpeedBuffCoroutine());
    }

    private IEnumerator SpeedBuffCoroutine()
    {
        float original = controller.moveSpeed;
        controller.moveSpeed += buffValue;
        yield return new WaitForSeconds(duration);
        controller.moveSpeed = original;
    }
}
