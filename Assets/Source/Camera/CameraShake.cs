using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _shakeDuration = 0.5f; // Time during which the shaking will take place.
    [SerializeField] private float _shakeStrength = 1f; // Shaking force.
    [SerializeField] private int _vibrateFrequency = 10; // Frequency of vibration during the shake (number of vibrations).
    [SerializeField] private float _randomShakeDirection = 30f; // Angle by which camera directions can be varied to create random shaking.

    // Invoke camera shake, using DoTween to move the camera in a random direction with specified parameters.
    public void Shake()
    {
        _cameraTransform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrateFrequency, _randomShakeDirection);
    }
}