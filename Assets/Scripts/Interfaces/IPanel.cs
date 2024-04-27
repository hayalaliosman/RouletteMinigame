using DG.Tweening;

public interface IPanel
{
    public void Open(float tweenDuration, Ease ease = Ease.OutBack);
    public void Close(float tweenDuration, Ease ease = Ease.InBack);
}
