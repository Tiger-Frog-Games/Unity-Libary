using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class SingletonGameObject : MonoBehaviour
    {
        //the singleton object
        public static SingletonGameObject Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                //Destorys any time another one of these is spawned
                Destroy(this);
            }
            else
            {
                //
                Instance = this;
            }
        }

     
    }
}