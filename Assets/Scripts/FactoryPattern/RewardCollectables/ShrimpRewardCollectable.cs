using UnityEngine;
using DG.Tweening;

public class ShrimpRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(360f, 0f, 0f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
