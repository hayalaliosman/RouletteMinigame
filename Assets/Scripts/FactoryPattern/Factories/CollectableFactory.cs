using System.Threading.Tasks;
using UnityEngine;

public abstract class CollectableFactory : MonoBehaviour
{
    public abstract Task<ICollectable> CreateCollectableAsync();
    public RouletteManager rouletteManager;
    [SerializeField] protected string prefabAddressableKey;
}
