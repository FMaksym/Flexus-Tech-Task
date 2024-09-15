using UnityEngine;

public class UIView : MonoBehaviour
{
    // Sets the slider value to the maximum firing force obtained from the bullet spavener.
    public void ChangeSliderValue(UIModel model)
    {
        model.shootPowerSlider.value = model.bulletSpawner.GetMaxShootPower();
    }
    
    // Sets the minimum and maximum values of the slider depending on the bullet spavener values.
    public void ChangeMinMaxShootPower(UIModel model)
    {
        model.shootPowerSlider.minValue = model.bulletSpawner.GetMinShootPower();
        model.shootPowerSlider.maxValue = model.bulletSpawner.GetMaxShootPower();
    }

    // Calls the Shoot method of the shoot controller.
    public void CannonShoot(UIModel model)
    {
        model.shootController.Shoot();
    }
}