using UnityEngine;
using DG.Tweening;

public class PineappleRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
        transform.DOPunchScale(Vector3.one * 1.5f, 1f, 8, 0.8f).SetEase(Ease.Linear);
    }
}
