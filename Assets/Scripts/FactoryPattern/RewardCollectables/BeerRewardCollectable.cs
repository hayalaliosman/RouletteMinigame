using DG.Tweening;
using UnityEngine;

public class BeerRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
}
