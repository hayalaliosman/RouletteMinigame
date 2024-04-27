using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWalletItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemAmountText;

    public void Initialize(Transform parent, int itemAmount)
    {
        var myTransform = transform;
        myTransform.SetParent(parent);
        myTransform.localPosition = Vector3.zero;
        // myTransform.SetSiblingIndex(walletItemIndex);
        myTransform.localScale = Vector3.one;
        itemAmountText.text = itemAmount.ToString();
    }
}
