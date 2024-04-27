using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.ResourceManagement.ResourceProviders;


public class BarbecuePartyManager : RouletteManager
{
    private int _collectedRewardAmount;
    public int CollectedRewardAmount
    {
        get => _collectedRewardAmount;
        set
        {
            _collectedRewardAmount = value;
            _collectedRewardAmount = Math.Max(0, _collectedRewardAmount);
            if(_collectedRewardAmount == spawnedCollectableCount)   CompleteRewardClaim();
        }
    }

    private new void Start()
    {
        base.Start();
        CreateRewards();
    }

    private void CreateRewards()
    {
        for (var i = 0; i < rewardPrefabAddresses.Count; i++)
        {
            var index = i; // Assigning i to another integer and using this value so that OnRewardInstantiateCompleted callback method will not use final value of i which is 14 in our case
            Addressables.InstantiateAsync(rewardPrefabAddresses[i]).Completed += handle => OnRewardInstantiateCompleted(handle, index);
        }
    }
    
    private void OnRewardInstantiateCompleted(AsyncOperationHandle<GameObject> obj, int slotIndex)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            var reward = obj.Result.transform;
            reward.SetParent(rewardSlots[slotIndex]);
            reward.localPosition = reward.localEulerAngles = Vector3.zero;
            reward.localScale = Vector3.one;
            reward.GetComponent<IInitizableRouletteMinigame>().Initialize(this);
            Debug.Log("Successfully instantiated addressable reward object.");
        }
        else
        {
            Debug.LogError("Failed to instantiate addressable object.");
        }
    }

    private IEnumerator ChooseRewardCoroutine()
    {
        const float traverseInterval = 0.1f;
        var wfs = new WaitForSeconds(traverseInterval);
        var loopCount = Random.Range(2, 4);
        var chosenRewardIndex = Random.Range(0, rewardSlots.Length);
        var iterationCount = loopCount * rewardSlots.Length + chosenRewardIndex;
        
        for (var i = 0; i < iterationCount; i++)
        {
            onRewardShined.Invoke(i % rewardSlots.Length);
            yield return wfs;
        }
        
        ClaimReward(iterationCount);
    }

    private void UpdateRewardIndexes()
    {
        for (var i = 0; i < rewardSlots.Length; i++) rewardSlots[i].transform.SetSiblingIndex(i);
        onRewardIndexesUpdated.Invoke();
    }

    private void CompleteMiniGame()
    {
        completeMinigameButton.transform.localScale = Vector3.zero;
        completeMinigameButton.SetActive(true);
        completeMinigameButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        onMiniGameCompleted.Invoke();
    }
    
    private void OnMainSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)    Debug.Log("Main scene loaded successfully");
        else Debug.LogError("Failed to load main scene");
    }
    
    private void CompleteRewardClaim()
    {
        CollectedRewardAmount = 0;
        claimedRewardCount++;
        if (claimedRewardCount == totalRewardCount) CompleteMiniGame();
        else StartCoroutine(ReactivateRewardSelection());
    }
    
    private IEnumerator ReactivateRewardSelection()
    {
        Utilities.ShuffleRewards(rewardSlots);
        UpdateRewardIndexes();
        const float replacementDuration = 1.3f;
        onRewardSelectionReactivated.Invoke(replacementDuration);
        
        yield return new WaitForSeconds(replacementDuration);
        
        chooseRewardButton.transform.localScale = Vector3.zero;
        chooseRewardButton.SetActive(true);
        chooseRewardButton.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);
    }
    
    public void OnClick_ChooseReward()
    {
        chooseRewardButton.SetActive(false);
        StartCoroutine(ChooseRewardCoroutine());
    }

    public void OnClick_CompleteMiniGame()
    {
        DOTween.CompleteAll();
        var handle = Addressables.LoadSceneAsync(mainSceneAddress);
        handle.Completed += OnMainSceneLoaded;
    }

    public override void ClaimReward(int rewardIndex)
    {
        onRewardClaimed.Invoke(rewardIndex % rewardSlots.Length);
    }
    
    public override void IncreaseCollectedRewardAmount() => CollectedRewardAmount++;
}
