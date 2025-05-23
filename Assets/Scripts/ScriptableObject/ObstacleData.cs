using UnityEngine;

public enum ObsType
{
    Damage,
    disturbance,
    fake
}

public class DamageObject
{
    public ObsType type;
    public int damage;
}

[CreateAssetMenu(fileName = "Obstacle", menuName = "NewObstacle")]
public class ObstacleData : ScriptableObject
{
    [Header("ObsInfo")]
    public string obsName;
    public string obsDescription;
    public ObsType type;

}
