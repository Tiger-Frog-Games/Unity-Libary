using System;
using UnityEngine;

namespace TigerFrogGames
{
    /// <summary>
    /// The point of this class is to run the game clock logic.
    /// It will broad cast an event whenever a min or an hour rolls over. 
    /// 
    /// It will reset when you reach a new day  OnNewDayStart()
    /// 
    /// It will pause when you are in a pause menu 
    ///     
    /// </summary>

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        #region Variables

        /// <summary>
        /// How many secounds in real time is one in game day.
        /// </summary>
        [field:SerializeField, Header("Time Scale")] public float dayLength { private set; get; } = 60 ;

        private float _minLength;
        
        public TimeSpan CurrentTime { private set; get;}
        
        [Header("Times of the day")]

        [SerializeField] private int startDay;
        [SerializeField] private int startHour;
        [SerializeField] private int startMin;
        [SerializeField] private int startSec;
        
        

        //These are events that other classes/gameObjects will use to determine the time.
        [Header("Events")]

        
        [SerializeField] private EventChannel OnTimerReset;

        [SerializeField] private EventChannelTimeSpan OnTimeChange;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                throw new System.Exception("An instance of this singleton already exists.");
            }
            Instance = this;
            
            

            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
            
            if (OnTimerReset != null)
            {
                OnTimerReset.OnEvent += ResetTimer;
            }

            changeTimeScale(dayLength);
            
            ResetTimer();
        }
        
        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            
            if (OnTimerReset != null)
            {
                OnTimerReset.OnEvent -= ResetTimer;
            }
        }

        private int _lastBroadcastedMin = -1;
        private void Update()
        {
            CurrentTime += TimeSpan.FromMinutes( Time.deltaTime * _minLength  );

            
            //You can change this seconds if you have a slower time scale
            if (CurrentTime.Minutes != _lastBroadcastedMin)
            {
                OnTimeChange.RaiseEvent(CurrentTime);
                _lastBroadcastedMin = CurrentTime.Minutes;
            }
        }
        
        #endregion

        #region Methods
        
        //Pauses Timer if the game is paused
        private void OnGameStateChanged(GameState newGameState)
        {
            enabled = newGameState == GameState.Gameplay;
        }
        
        //reset
        [ContextMenu("Reset Timer")]
        public void ResetTimer()
        {
            setTime(startDay, startHour, startMin, startSec);
        }

        public void setTime(int day,int hour, int minutes, int sec)
        {
            CurrentTime = new TimeSpan(day,hour,minutes,sec);
        }

        /// <summary>
        /// Changes the length of the day based on how many secounds real time are in a single day. 
        /// </summary>
        /// <param name="dayLength"></param>
        public void changeTimeScale(float dayLengthI)
        {
            if(dayLength == 0) return;
            
            
            _minLength = 1440 / dayLengthI ;
        }
        
        #endregion

        #region Editor

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (dayLength == 0)
            {
                Debug.Log("Day length can not be zero! or you will divide by zero");
            }
        }

#endif

        #endregion
    }
}