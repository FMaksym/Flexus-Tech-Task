using UnityEngine;

public class CannonTouchRotation : MonoBehaviour
{
    [SerializeField] private GameObject _cannonBase;

    [Header("Input Parametres")]
    [SerializeField] private Vector2 _clampInDegrees = new Vector2(180f, 90f);
    [SerializeField] private Vector2 _sensitivity = new Vector2(0.1f, 0.1f);
    [SerializeField] private Vector2 _smoothing = new Vector2(1f, 1f);

    private Vector2 _touchFinal;
    private Vector2 _smoothTouch;
    private PlayerInput _inputActions;
    private ITouchInput _touchInputProvider;
    private IRotationHandler _rotationHandler;

    private void OnEnable()
    {
        // Initialize user input and enable 
        _inputActions = new PlayerInput();
        _inputActions.Enable();

        // Configuring the touch input provider and rotation handler
        _touchInputProvider = new TouchInputProvider(_inputActions.PlayerActionMap.TouchRotation);
        _rotationHandler = new CannonRotationHandler(transform, _cannonBase);
    }

    private void LateUpdate()
    {
        if (_touchInputProvider.IsTouchingUI()) return; // If touching occurs on UI, ignore it

        // Get the touching change and update the total value for rotation
        Vector2 touchDelta = _touchInputProvider.GetTouchDelta();
        _touchFinal += ScaleAndSmooth(touchDelta);
        _rotationHandler.ApplyRotation(_touchFinal, _cannonBase, _clampInDegrees);
    }

    // Method for scaling and smoothing input touch data
    private Vector2 ScaleAndSmooth(Vector2 delta)
    {
        delta = Vector2.Scale(delta, new Vector2(_sensitivity.x * _smoothing.x, _sensitivity.y * _smoothing.y)); // Scale touch data based on sensitivity and smoothing 

        // Apply smoothing to input data
        _smoothTouch.x = Mathf.Lerp(_smoothTouch.x, delta.x, 1f / _smoothing.x);
        _smoothTouch.y = Mathf.Lerp(_smoothTouch.y, delta.y, 1f / _smoothing.y);
        return _smoothTouch;
    }

    // Disable input actions when the object is turned off
    private void OnDisable()
    {
        _inputActions.Disable();
    }
}