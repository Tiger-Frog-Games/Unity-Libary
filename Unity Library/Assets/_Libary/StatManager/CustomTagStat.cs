using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Tag/Stat")]
    public class CustomTagStat : ScriptableObject
    {
        [field: SerializeField] public string TagName { get; private set; }
        
        #if UNITY_EDITOR
        [field: SerializeField] public bool CanBeUsedAsInstantStatusEffect { get; private set; } = false;
        #endif
    }
}