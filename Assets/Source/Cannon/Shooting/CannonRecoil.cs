using DG.Tweening;
using UnityEngine;

public class CannonRecoil : MonoBehaviour
{
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private float recoilDistance = 0.5f;
    [SerializeField] private float recoilDuration = 0.2f;

    private Vector3 originalPosition;

    // Initialization of the initial position of the gun at start// Method for performing gun recoil
    private void Start()
    {
        originalPosition = cannonTransform.localPosition;
    }

    public void Recoil()
    {
        // Calculate the recoil position as an offset from the original gun position
        Vector3 recoilPosition = originalPosition - cannonTransform.forward * recoilDistance;

        // Perform the recoil motion and bring the cannon back.
        cannonTransform.DOLocalMove(recoilPosition, recoilDuration)
            .OnComplete(() => cannonTransform.DOLocalMove(originalPosition, recoilDuration));
    }
}