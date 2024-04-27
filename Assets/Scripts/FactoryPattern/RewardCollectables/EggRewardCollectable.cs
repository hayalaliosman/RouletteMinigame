using UnityEngine;
using DG.Tweening;

public class EggRewardCollectable : RewardCollectable
{
    private void Start()
    {
        onCollectBegun.AddListener(OnCollectBegun);
    }

    private void OnCollectBegun()
    {
        transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }
}
