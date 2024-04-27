using UnityEngine;
using DG.Tweening;

public class NoodleRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOScale(Vector3.one * 1.2f ,0.4f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
