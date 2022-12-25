using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class UIStatBar : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image healthBarSprite;
        [SerializeField] private StatBlock statBlock;

        [SerializeField] private float changeSpeed = 2;
        private float _hp;
        private float _maxHp;
        
        [SerializeField] private CustomTagStat hpTag;
        [SerializeField] private CustomTagStat maxHpTag;
        
        #endregion

        #region Unity Methods

        private void Start()
        {
            statBlock.GetStat(hpTag).OnStatChange += OnHpStatChange;
            statBlock.GetStat(maxHpTag).OnStatChange += OnMaxHpStatChange;

            _hp = statBlock.GetStatValue(hpTag);
            _maxHp = statBlock.GetStatValue(maxHpTag);

            healthBarSprite.fillAmount = _hp / _maxHp;
        }
        
        private void OnDestroy()
        {
            if(statBlock.GetStat(hpTag) != null) statBlock.GetStat(hpTag).OnStatChange -= OnHpStatChange;
            
            
            if(statBlock.GetStat(maxHpTag) != null)statBlock.GetStat(maxHpTag).OnStatChange -= OnMaxHpStatChange;
        }


        private void Update()
        {
            healthBarSprite.fillAmount =
                Mathf.MoveTowards(healthBarSprite.fillAmount, _hp/_maxHp, Time.deltaTime * changeSpeed);
        }

        #endregion

        #region Methods

        private void OnHpStatChange(float obj)
        {
            _hp = obj;
        }
        
        private void OnMaxHpStatChange(float obj)
        {
            _maxHp = obj;
        }
        
        #endregion
    }
}