using UnityEngine;

public interface Iinteractable_Obstacle
{
    public string GetInteractPrompt_Obstacle();

    public void OnInteract_Obstacle();
}

public class Obstacle : MonoBehaviour, Iinteractable_Obstacle
{
    public ObstacleData data;

    public string GetInteractPrompt_Obstacle()
    {
        string str = $"{data.obsName}\n{data.obsDescription}";
        return str;
    }

    public void OnInteract_Obstacle()
    {
        CharacterManager.Instance.Player.obsData = data;
        CharacterManager.Instance.Player.obsInfo?.Invoke();
    }
}
