using UnityEngine;

public interface ITouchInput
{
    // Method to get the touch change (delta) in the current frame
    // Returns a vector representing the difference in touch position from the previous frame
    Vector2 GetTouchDelta();

    // Method to check if the touch occurs on the user interface (UI)
    // Returns true if the touch occurs on the UI, otherwise false
    bool IsTouchingUI();
}