using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class CustomTag : MonoBehaviour
    {
        #region Variables

        [field: SerializeField] public List<CustomTagScriptableObject> Tags {  get; private set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public bool HasTag(CustomTagScriptableObject t)
        {
            return Tags.Contains(t);
        }

        public void AddTag(CustomTagScriptableObject t)
        {
            if (!Tags.Contains(t))
            {
                Tags.Add(t);
            }
            
        }


        public static bool HasTag(GameObject obj, CustomTagScriptableObject t)
        {
            return obj.TryGetComponent<CustomTag>(out var tags) && tags.HasTag(t);
        }
        
        public bool RemoveTag(CustomTagScriptableObject t)
        {
            return Tags.Remove(t);
        }
        
        #if UNITY_EDITOR

        [ContextMenu("Print Tags")]
        public void printTags()
        {
            string result = "Tags :";
            foreach (var Tag in Tags)
            {
                result += " " + Tag.TagName;
            }

            print(result);
        }
        
        #endif
        
        #endregion
    }
}