using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchInputProvider : ITouchInput
{
    private InputAction _touchInputAction;

    // Constructor that initializes TouchInputProvider with specified InputAction
    public TouchInputProvider(InputAction touchInputAction)
    {
        _touchInputAction = touchInputAction;
    }

    // Initialize or update InputAction
    public void Initialize(InputAction touchInputAction)
    {
        _touchInputAction = touchInputAction;
    }

    // Get the touch change (delta) in the current frame
    // Returns a vector representing the difference in touch position from the previous frame
    public Vector2 GetTouchDelta()
    {
        return _touchInputAction.ReadValue<Vector2>();
    }

    // Check if the current touch is happening on the UI
    // Returns true if the touching occurs on the UI, otherwise false 
    public bool IsTouchingUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}