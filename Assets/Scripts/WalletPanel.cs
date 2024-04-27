using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WalletPanel : MonoBehaviour, IPanel
{
    [SerializeField] private RectTransform background;
    [SerializeField] private Image panelImage;

    private float _initialPanelAlpha;

    private void Awake() => _initialPanelAlpha = panelImage.color.a;

    public void Open(float tweenDuration, Ease ease = Ease.OutBack)
    {
        gameObject.SetActive(true);
        background.localScale = Vector3.zero;
        background.DOScale(Vector3.one, tweenDuration).SetEase(ease);
    }

    public void Close(float tweenDuration, Ease ease = Ease.InBack)
    {
        panelImage.DOFade(0f, tweenDuration).SetEase(ease);
        background.DOScale(Vector3.zero, tweenDuration).SetEase(ease)
            .OnComplete(() =>
            {
                Utilities.ChangeAlpha(panelImage, _initialPanelAlpha);
                gameObject.SetActive(false);
            });
    }
}
