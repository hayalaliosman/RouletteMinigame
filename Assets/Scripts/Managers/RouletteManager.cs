using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// Base class for roulette mini game
/// </summary>
public abstract class RouletteManager : MonoBehaviour
{
    public RectTransform mainCanvas;
    public Transform collectableDestinationPoint;
    public CollectableFactory[] collectableFactories;
    public Wallet wallet;
    
    [HideInInspector] public List<Reward> rewards = new();
    [HideInInspector] public UnityEvent<int> onRewardShined, onRewardClaimed;
    [HideInInspector] public UnityEvent<float> onRewardSelectionReactivated;
    [HideInInspector] public UnityEvent onRewardIndexesUpdated, onMiniGameCompleted;
    
    [SerializeField] protected string mainSceneAddress;
    [SerializeField] protected int totalRewardCount = 3;
    [SerializeField] protected int spawnedCollectableCount = 10;
    [SerializeField] protected GameObject chooseRewardButton, completeMinigameButton;
    [SerializeField] protected List<string> rewardPrefabAddresses;
    [SerializeField] protected Transform[] rewardSlots;
    protected int claimedRewardCount = 0;
    
    private readonly List<Vector3> _rewardSlotPositions = new();

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }
    
    protected void Start()
    {
        Utilities.ShuffleRewards(rewardPrefabAddresses); // Shuffle reward list first to randomize their positions
        foreach (var rewardSlot in rewardSlots) _rewardSlotPositions.Add(rewardSlot.localPosition);
    }
    
    public Vector3 GetRewardSlotPositionByIndex(int index)
    {
        return _rewardSlotPositions[index];
    }
    
    /// <summary>
    /// Creates reward collectables which will move into the wallet with animation
    /// </summary>
    /// <param name="rewardIndex">Type of the reward as integer</param>
    public void CreateRewardCollectables(int rewardIndex)
    {
        for (var i = 0; i < spawnedCollectableCount; i++) collectableFactories[rewardIndex].CreateCollectableAsync();
    }

    public abstract void IncreaseCollectedRewardAmount();
    
    public abstract void ClaimReward(int rewardIndex);
}
