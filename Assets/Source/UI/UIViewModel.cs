using UnityEngine;

public class UIViewModel : MonoBehaviour
{
    public UIModel uiModel; // Reference to UI model
    public UIView uiView; // Reference to the UI view

    // Set the minimum and maximum values of the slider and the current value of the slider at startup
    private void Start()
    {
        uiView.ChangeMinMaxShootPower(uiModel); 
        uiView.ChangeSliderValue(uiModel); 
    }

    // Called when the firing button is clicked.
    public void OnClickShoot()
    {
        uiView.CannonShoot(uiModel); // Start the firing process through the view
    }
}