using UnityEngine;
using DG.Tweening;

public class HotdogRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOPunchScale(Vector3.one * 1.15f ,0.2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
