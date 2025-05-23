using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObstacleTrigger : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private Iinteractable_Obstacle curInteractable_Obstacle;

    public TextMeshProUGUI promptText;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable_Obstacle = hit.collider.GetComponent<Iinteractable_Obstacle>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable_Obstacle = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable_Obstacle.GetInteractPrompt_Obstacle();
    }
}
