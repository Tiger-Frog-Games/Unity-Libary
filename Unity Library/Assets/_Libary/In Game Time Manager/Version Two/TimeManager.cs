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
        private static TimeManager _instance;
        public static TimeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeManager();
                }
                return _instance;
            }
        }

        #region Variables

        /// <summary>
        /// How many secounds in real time is one in game day.
        /// </summary>
        [SerializeField] private float _dayLength;

        private float _minLength;
        
        private TimeSpan _currentTime;
        
        [Header("Times of the day")]

        [SerializeField] private int startDay;
        [SerializeField] private int startHour;
        [SerializeField] private int startMin;
        
        

        //These are events that other classes/gameObjects will use to determine the time.
        [Header("Events")]

        
        [SerializeField] private EventChannel OnTimerReset;

        [SerializeField] private EventChannelTimeSpan OnTimeChange;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _instance = this;

            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
            
            if (OnTimerReset != null)
            {
                OnTimerReset.OnEvent += ResetTimer;
            }

            _minLength = 1440 / _dayLength   ;
            
            ResetTimer();
        }

        private void Start()
        {
            //dayChange?.RaiseEvent(currentDay_InGame);
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            
            if (OnTimerReset != null)
            {
                OnTimerReset.OnEvent -= ResetTimer;
            }
        }
        
        private void Update()
        {
            _currentTime += TimeSpan.FromMinutes( Time.deltaTime * _minLength  );
            
            OnTimeChange.RaiseEvent(_currentTime);
            
            //print(_currentTime.ToString(@"d\.hh\:mm\:ss"));
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
        private void ResetTimer()
        {
            _currentTime = new TimeSpan(startDay,startHour,startMin,startHour);
        }

        #endregion

        #region Editor

   

        #endregion
    }
}