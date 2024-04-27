using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{
    public string barbecuePartySceneAddress;

    [SerializeField] private WalletPanel walletPanel;

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)    Debug.Log("Barbecue scene loaded successfully");
        else Debug.LogError("Failed to load barbecue scene");
    }

    public void OnClick_EnterBarbequeParty()
    {
        DOTween.CompleteAll();
        var handle = Addressables.LoadSceneAsync(barbecuePartySceneAddress);
        handle.Completed += OnSceneLoaded;
    }

    public void OnClick_OpenWallet() => walletPanel.Open(0.5f);

    public void OnClick_CloseWallet() => walletPanel.Close(0.4f);
}
