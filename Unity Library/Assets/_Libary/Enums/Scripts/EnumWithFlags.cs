using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    //Need to assign a value of a power of 2.
    [Flags]
    public enum MultipleSelect
    {
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8
    }

    public class EnumWithFlags : MonoBehaviour
    {
        public static bool doesContain( MultipleSelect one, MultipleSelect two )
        {
            //Use bitwise and to see if they contain a similiar element
            return (one & two) != 0;
        }

        [SerializeField] private MultipleSelect oneTest;
        [SerializeField] private MultipleSelect twoTest;
        
        public void test()
        {
            Debug.Log(doesContain(oneTest,twoTest));
        }
        
        
        
    }
}