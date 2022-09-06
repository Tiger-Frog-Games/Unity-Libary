using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public static class ListUtilities
    {
        #region Variables

        #endregion

        #region Unity Methods

        #endregion

        #region Methods
        
        
        /// <summary>
        /// Returns a different int then the last index used. 
        /// </summary>
        /// <param name="lastIndex"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static int GetNoneRepeatingIndex(int lastIndex, int capacity)
        {
            return (lastIndex + Random.Range(1, capacity -1)) % capacity;
        }

        //Found on unity forums https://answers.unity.com/questions/1307074/how-do-i-compare-two-lists-for-equality-not-caring.html
        
        /// <summary>
        /// Returns true if the lists are the same. Accounts for the lists being unordered and accounts for dublicates. 
        /// </summary>
        /// <param name="aListA"></param>
        /// <param name="aListB"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool CompareLists<T>(List<T> aListA, List<T> aListB)
        {
            if (aListA == null || aListB == null || aListA.Count != aListB.Count)
                return false;
            if (aListA.Count == 0)
                return true;
            Dictionary<T, int> lookUp = new Dictionary<T, int>();
            // create index for the first list
            for(int i = 0; i < aListA.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListA[i], out count))
                {
                    lookUp.Add(aListA[i], 1);
                    continue;
                }
                lookUp[aListA[i]] = count + 1;
            }
            for (int i = 0; i < aListB.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListB[i], out count))
                {
                    // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                    return false;
                }
                count--;
                if (count <= 0)
                    lookUp.Remove(aListB[i]);
                else
                    lookUp[aListB[i]] = count;
            }
            // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
            return lookUp.Count == 0;
        }
        
        
        #endregion
    }
}