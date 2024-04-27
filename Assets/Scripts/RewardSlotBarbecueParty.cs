using DG.Tweening;
using UnityEngine;

public class RewardSlotBarbecueParty : RewardSlot
{
    private new void Start()
    {
        base.Start();
        // Mini game specific codes can be written here
    }

    /// <summary>
    /// Mini game specific activation of shuffle animation
    /// </summary>
    /// <param name="animationDuration">Duration of shuffle animation</param>
    protected override void ActivateShuffleAnimation(float animationDuration)
    {
        slotIndex = transform.GetSiblingIndex();
        
        const int loopCount = 1;
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f * loopCount), animationDuration / loopCount, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        
        var destination = rouletteManager.GetRewardSlotPositionByIndex(slotIndex);
        transform.DOLocalJump(destination, Random.Range(-500f, 500f), 1, animationDuration).SetEase(Ease.Linear);
    }
}
