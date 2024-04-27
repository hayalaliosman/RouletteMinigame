using UnityEngine;
using DG.Tweening;

public class GodfoodRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOScale(Vector3.one * 1.5f ,0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
