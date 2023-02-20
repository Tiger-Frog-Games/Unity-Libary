using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectHandlerRpg : StatusEffectHandler
    {
        #region Variables

        [SerializeField] private CustomTagStat hp;
        [SerializeField] private CustomTagStat hpMax;
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public override void AddStatusEffectInstant(StatusEffectInstant newEffect)
        {
            if(newEffect.StatToEffect ==  hp)
            {
                var newHp = Mathf.Clamp(_statBlock.GetStatValue(hp) + newEffect.Value, 0, _statBlock.GetStatValue(hpMax));
                
                _statBlock.ChangeStat(hp,newHp);
            }
            else if(newEffect.StatToEffect ==  hpMax)
            {
                var newMaxHp = Mathf.Clamp(_statBlock.GetStatValue(hp) + newEffect.Value, 0, _statBlock.GetStatValue(hpMax));
                
                //changes the unit hp to be less then or equal to the new max hp
                var newHp = Mathf.Min(_statBlock.GetStatValue(hp), newMaxHp);

                newEffect.ChangValue(newMaxHp);
                _statBlock.ChangeStat(hpMax,newMaxHp);
                _statBlock.ChangeStat(hp, newHp);
            }
            
            
            
            if (newEffect.IsPermanentChange == false)
            {
                _temporaryStatChanges.Add(newEffect);
            }
        }

        #endregion
    }
}