using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIModel : MonoBehaviour
{
    public Slider shootPowerSlider; // Slider for adjusting the firing force

    [Inject, HideInInspector] public ShootController shootController; // Shooting controller, embeddable via Zenject
    [Inject, HideInInspector] public BulletSpawner bulletSpawner; // Bullet spavener embedded via Zenject

    public static event Action<float> OnFirePowerChanged; // Event that is called when changing the firing force

    private void OnEnable()
    {
        shootPowerSlider.onValueChanged.AddListener(HandleSliderValueChanged); // Subscribe to the slider value change event
    }

    private void HandleSliderValueChanged(float value)
    {
        OnFirePowerChanged?.Invoke(value); // Call the event when the slider value changes
    }

    private void OnDisable()
    {
        // Unsubscribe from the slider value change event
        shootPowerSlider.onValueChanged.RemoveListener(HandleSliderValueChanged);
    }
}