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

        private int _lastBroadcastedMin = -1;
        private void Update()
        {
            _currentTime += TimeSpan.FromMinutes( Time.deltaTime * _minLength  );

            if (_currentTime.Minutes != _lastBroadcastedMin)
            {
                OnTimeChange.RaiseEvent(_currentTime);
                _lastBroadcastedMin = _currentTime.Minutes;
            }

            //
            //print($"{_currentTime.Days} - {_currentTime.Hours}:{_currentTime.Minutes}.{_currentTime.Seconds}"
            
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
            setTime( startDay, startHour, startMin);
        }

        private void setTime(int day,int hour, int minutes)
        {
            _currentTime = new TimeSpan(day,hour,minutes,0);
        }
        
        #endregion

        #region Editor

   

        #endregion
    }
}