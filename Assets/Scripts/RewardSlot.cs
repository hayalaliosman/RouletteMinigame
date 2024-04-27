using UnityEngine;

public abstract class RewardSlot : MonoBehaviour
{
    [SerializeField] protected RouletteManager rouletteManager;

    protected int slotIndex;
    
    protected void Start()
    {
        slotIndex = transform.GetSiblingIndex();
        rouletteManager.onRewardSelectionReactivated.AddListener(ActivateShuffleAnimation);
    }
    
    protected abstract void ActivateShuffleAnimation(float replacementDuration);
}
