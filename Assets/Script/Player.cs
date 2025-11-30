using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { private set; get; }
    public Transform lineTransform;
    [HideInInspector] public bool canThrow;
    [HideInInspector] public float offset;
    private float speed;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        canThrow = true;
        speed = 5.0f;
        offset = transform.position.x - lineTransform.position.x;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isPaused)
        {
            HandleMovement();
            HandleThrow();
        }
    }

    private void HandleMovement()
    {
        Vector3 newpos = new Vector3(transform.position.x + (InputManager.instance.moveInput.x * speed * Time.deltaTime), transform.position.y, 0.0f);
        newpos.x = Mathf.Clamp(newpos.x, BoundaryController.instance.currentLeftBoundary, BoundaryController.instance.currentRightBoundary);
        transform.position = newpos;
    }

    private void HandleThrow()
    {
        if (InputManager.instance.isThrowPressed && SpawnManager.instance.currentPlanet != null && canThrow)
        {
            GameManager.instance.NotifyObserver(PlayerAction.Throw);
            SpawnManager.instance.SpawnPhysicsPlanet();
            canThrow = false;
        }
    }
}
