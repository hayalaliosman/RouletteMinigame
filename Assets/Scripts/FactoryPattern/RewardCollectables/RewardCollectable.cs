using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public class RewardCollectable : MonoBehaviour, ICollectable, IInitizableRouletteMinigame
{
    protected readonly UnityEvent onCollectBegun = new();
    
    private RouletteManager _rouletteManager;
    
    public void Collect(Transform mainCanvas)
    {
        onCollectBegun.Invoke();
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.SetParent(mainCanvas);
        rectTransform.localPosition = Vector3.zero;
        var initialPosition = rectTransform.position;
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomPower = Random.Range(50f, 300f);
        rectTransform.DOAnchorPos(rectTransform.anchoredPosition + randomPower * randomDirection, Random.Range(0.35f, 0.65f)).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                var path = new Vector3[2];
                path[0] = (rectTransform.position + initialPosition) * 0.5f;
                path[1] = _rouletteManager.collectableDestinationPoint.position;
                rectTransform.SetParent(_rouletteManager.collectableDestinationPoint);
                rectTransform.DOPath(path, 1f, PathType.CatmullRom).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    transform.DOKill();
                    _rouletteManager.IncreaseCollectedRewardAmount();
                    // Destroying gameObject which was created as addressable
                    Addressables.ReleaseInstance(gameObject);
                });
            });
    }
    
    public void Initialize(RouletteManager rouletteManager)
    {
        _rouletteManager = rouletteManager;
    }
}
