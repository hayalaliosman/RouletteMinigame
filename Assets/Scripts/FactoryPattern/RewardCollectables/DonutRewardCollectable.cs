using UnityEngine;
using DG.Tweening;

public class DonutRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOScale(Vector3.one * 1.25f ,0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
