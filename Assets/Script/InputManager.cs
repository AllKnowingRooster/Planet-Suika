using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction throwAction;

    public static InputManager instance { private set; get; }
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool isThrowPressed;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();
        moveAction = actionMap.FindAction("Move");
        moveAction.Enable();
        throwAction = actionMap.FindAction("Throw");
        throwAction.Enable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        isThrowPressed = throwAction.WasPressedThisFrame();
    }

}
