using UnityEngine;

[CreateAssetMenu(fileName = "NewUIWalletItemInformation", menuName = "UIWalletItemInformation")]
public class UIWalletItemInformation : ScriptableObject
{
    public string uIWalletItemPrefabAddress;
    public Reward.RewardType itemType;
}
