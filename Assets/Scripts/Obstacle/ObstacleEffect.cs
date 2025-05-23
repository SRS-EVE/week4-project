using UnityEngine;

public interface IObstacleEffect
{
    void ApplyEffect(GameObject target);
}

public class ObstacleEffect : MonoBehaviour, IObstacleEffect
{
    public void ApplyEffect(GameObject target)
    {

    }
}
