using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "Tag/CustomTag")]
    public class CustomTagScriptableObject : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
    }
}