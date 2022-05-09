using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public interface IState 
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}