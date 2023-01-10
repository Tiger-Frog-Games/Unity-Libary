using System;

namespace TigerFrogGames
{
    public abstract class StatusEffect 
    {
        #region Variables
        
        //ID?

        public Guid ID { private set; get;}
        
        //Name?
        
        #endregion
        
        #region Methods

        protected StatusEffect()
        {
            ID = Guid.NewGuid();
        }
        
        public virtual void Reset()
        {
            
        }
        
        public abstract StatusEffect Clone();

        #endregion

    }
}