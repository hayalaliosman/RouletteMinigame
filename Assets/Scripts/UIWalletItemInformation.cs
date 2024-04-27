using UnityEngine;

[CreateAssetMenu(fileName = "NewUIWalletItemInformation", menuName = "UIWalletItemInformation")]
public class UIWalletItemInformation : ScriptableObject
{
    public string itemPrefabAddress;
    public Reward.RewardType itemType;
}
