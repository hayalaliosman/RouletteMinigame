using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Transform uIWalletsParent;
    [SerializeField] private Transform[] walletSlots;
    [SerializeField] private UIWalletItemInformation[] uIWalletItemInformations; // Scriptable object array
    
    private WalletItemList _walletItemList = new WalletItemList();
    
    [Serializable]
    public class WalletItemList
    {
        public List<WalletItem> walletItems = new();
    }
    
    [Serializable]
    public class WalletItem
    {
        public Reward.RewardType rewardType;
        public int rewardAmount;

        public WalletItem(Reward.RewardType rewardType, int rewardAmount)
        {
            this.rewardType = rewardType;
            this.rewardAmount = rewardAmount;
        }
    }

    private void Start()
    {
        Load();
        if (uIWalletsParent) CreateUIWalletItems();
    }

    private void Save()
    {
        var json = JsonUtility.ToJson(_walletItemList);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/wallet.json", json);
    }

    private void Load()
    {
        Debug.Log("Wallet load");
        var path = Application.persistentDataPath + "/wallet.json";
        if (System.IO.File.Exists(path))
        {
            Debug.Log("Json path exists");
            var json = System.IO.File.ReadAllText(path);
            _walletItemList = JsonUtility.FromJson<WalletItemList>(json);
            Debug.Log("Wallet item count= " + _walletItemList.walletItems.Count);
        }
        else
        {
            Debug.Log("Json path does not exist");
        }
    }

    private string FindUIWalletPrefabAddress(Reward.RewardType itemType)
    {
        return uIWalletItemInformations[(int)itemType].uIWalletItemPrefabAddress;
    }
    
    private void CreateUIWalletItems()
    {
        for (var i = 0; i < _walletItemList.walletItems.Count; i++)
        {
            var index = i; // Assigning i to another integer and using this value so that OnRewardInstantiateCompleted callback method will not use final value of i which is 14 in our case
            var prefabAddress = FindUIWalletPrefabAddress(_walletItemList.walletItems[index].rewardType);
            Addressables.InstantiateAsync(prefabAddress).Completed += handle => OnUIWalletItemInstantiateCompleted(handle, index, _walletItemList.walletItems[index].rewardAmount);
        }
    }
    
    private void OnUIWalletItemInstantiateCompleted(AsyncOperationHandle<GameObject> obj, int walletItemIndex, int rewardAmount)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            var uIWalletItem = obj.Result.transform;
            uIWalletItem.GetComponent<UIWalletItem>().Initialize(walletSlots[walletItemIndex], rewardAmount);
            // uIWalletItem.GetComponent<UIWalletItem>().Initialize(uIWalletsParent, walletItemIndex, rewardAmount);
            
            Debug.Log("Successfully instantiated addressable reward object.");
        }
        else
        {
            Debug.LogError("Failed to instantiate addressable object.");
        }
    }

    public void EarnReward(WalletItem newWalletItem)
    {
        var isSameWalletItemFound = false;
        foreach (var walletItem in _walletItemList.walletItems.Where(walletItem => walletItem.rewardType == newWalletItem.rewardType))
        {
            isSameWalletItemFound = true;
            walletItem.rewardAmount += newWalletItem.rewardAmount;
            break;
        }
        if(!isSameWalletItemFound)  _walletItemList.walletItems.Add(newWalletItem);
        Save();
    }
}
