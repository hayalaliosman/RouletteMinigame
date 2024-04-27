using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class Reward : MonoBehaviour, IInitizableRouletteMinigame
{
    [SerializeField] private Image glowImage, claimedImage, grayImage;
    [SerializeField] private Transform tickIcon;
    [SerializeField] private CanvasGroup content;
    [SerializeField] private string collectablePrefabAddress;
    [SerializeField] private RewardType rewardType;

    private RouletteManager _rouletteManager;
    private Transform _rewardSlot;
    private int _rewardIndex;
    private bool _rewardClaimed;

    public enum RewardType
    {
        Beer,
        Candy,
        Coconut,
        Croissant,
        Donut,
        Egg,
        Fig,
        Godfood,
        Hotchocolate,
        Hotdog,
        Mushroom,
        Noodle,
        Pineapple,
        Shrimp
    }

    private void ClaimReward(int claimedRewardIndex)
    {
        if (_rewardIndex != claimedRewardIndex) return;
        if (_rewardClaimed)
        {
            _rouletteManager.ClaimReward(_rewardIndex+1); // If this reward is already claimed, claim the next reward
            return;
        }
        
        glowImage.gameObject.SetActive(true);
        glowImage.DOFade(1f, 0.15f).SetEase(Ease.Linear).SetLoops(10, LoopType.Yoyo).OnComplete(CompleteClaim);
    }

    private void CompleteClaim()
    {
        tickIcon.localScale = Vector3.zero;
        tickIcon.gameObject.SetActive(true);
        glowImage.DOFade(0f, 0.4f).SetEase(Ease.Linear);
        claimedImage.gameObject.SetActive(true);
        const float tweenDuration = 1f;
        claimedImage.DOFade(1f, tweenDuration * 0.5f).SetEase(Ease.Linear);
        tickIcon.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _rewardClaimed = true;
                CreateCollectables();
                var walletItem = new Wallet.WalletItem(rewardType, 1);
                _rouletteManager.wallet.EarnReward(walletItem);
            });
    }

    private void CreateCollectables()
    {
        var rewardIndex = (int)rewardType;
        _rouletteManager.CreateRewardCollectables(rewardIndex);
    }

    private void ActivateShine(int shinedRewardIndex)
    {
        if (_rewardIndex != shinedRewardIndex) return;

        glowImage.gameObject.SetActive(true);
        glowImage.DOComplete();
        glowImage.DOFade(1f, 0.3f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo)
            .OnComplete(() =>
            {
                glowImage.gameObject.SetActive(false);
            });
    }
    
    private void OnRewardIndexUpdated()
    {
        _rewardIndex = _rewardSlot.GetSiblingIndex();
    }

    private void OnMiniGameCompleted()
    {
        const float tweenDuration = 1f;
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f), tweenDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack).
            OnComplete(() =>
            {
                Addressables.ReleaseInstance(gameObject);
            });
    }

    private void OnRewardSelectionReactivated(float replacementDuration)
    {
        if (!_rewardClaimed || grayImage.gameObject.activeSelf) return;

        content.DOFade(0f, replacementDuration).SetEase(Ease.Linear).OnComplete(()=> content.gameObject.SetActive(false));
        grayImage.gameObject.SetActive(true);
        Utilities.ChangeAlpha(grayImage, 0f);
        grayImage.DOFade(1f, replacementDuration).SetEase(Ease.Linear);
    }

    public void Initialize(RouletteManager rouletteManager)
    {
        _rouletteManager = rouletteManager;
        _rouletteManager.onRewardShined.AddListener(ActivateShine);
        _rouletteManager.onRewardClaimed.AddListener(ClaimReward);
        _rouletteManager.onRewardIndexesUpdated.AddListener(OnRewardIndexUpdated);
        _rouletteManager.onMiniGameCompleted.AddListener(OnMiniGameCompleted);
        _rouletteManager.onRewardSelectionReactivated.AddListener(OnRewardSelectionReactivated);
        _rouletteManager.rewards.Add(this);
        _rewardSlot = transform.parent;
        _rewardIndex = _rewardSlot.GetSiblingIndex();
    }
}
