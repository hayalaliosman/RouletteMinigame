using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class RewardSlot : MonoBehaviour
{
    // [SerializeField] private BarbecuePartyManager barbecuePartyManager;
    [SerializeField] private RouletteManager rouletteManager;

    private int _slotIndex;
    
    private void Start()
    {
        _slotIndex = transform.GetSiblingIndex();
        // barbecuePartyManager.onRewardSelectionReactivated.AddListener(OnRewardSelectionReactivated);
        rouletteManager.onRewardSelectionReactivated.AddListener(OnRewardSelectionReactivated);
    }

    private void OnRewardSelectionReactivated(float replacementDuration)
    {
        _slotIndex = transform.GetSiblingIndex();
        const int loopCount = 1;
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f * loopCount), replacementDuration / loopCount, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        
        var destination = rouletteManager.GetRewardSlotPositionByIndex(_slotIndex);
        transform.DOLocalJump(destination, Random.Range(-500f, 500f), 1, replacementDuration).SetEase(Ease.Linear);
    }
}
