using UnityEngine;
using DG.Tweening;

public class CoconutRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
