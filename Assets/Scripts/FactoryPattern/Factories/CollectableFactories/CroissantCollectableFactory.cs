using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CroissantCollectableFactory : CollectableFactory
{
    public override async Task<ICollectable> CreateCollectableAsync()
    {
        var operation = Addressables.InstantiateAsync(prefabAddressableKey);

        await operation.Task;

        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            ICollectable collectable = operation.Result.GetComponent<CroissantRewardCollectable>();
            if (collectable != null)
            {
                operation.Result.GetComponent<IInitizableRouletteMinigame>().Initialize(rouletteManager);
                collectable.Collect(rouletteManager.mainCanvas);
                return collectable;
            }
            else
            {
                Debug.LogError("ICollectable component not found on instantiated object.");
            }
        }
        else
        {
            Debug.LogError($"Failed to instantiate the object: {prefabAddressableKey}");
        }

        return null;
    }
}
