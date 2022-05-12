using System.Collections;
using System.Collections.Generic;
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
        /// How fast the ingame clock with progress
        /// </summary>
        [SerializeField] private float gameRate;

        [Header("Times of the day")]

        [SerializeField] private int startDay;
        [SerializeField] private int startHour;
        [SerializeField] private int startMin;

        //[SerializeField] private int endHour;
        //[SerializeField] private int endMin;

        [Header("Current Time")]

        [SerializeField] private int currentDay_InGame;
        [SerializeField] private int currentHour_InGame;
        [SerializeField] private int currentMin_InGame;

        
        public int startingTime { get; private set; }
        public int noonTime { get; private set; }
        public int nightTime { get; private set; }

        private float currentTime_InGame;

        //These are events that other classes/gameObjects will use to determine the time.
        [Header("Events")]

        [SerializeField] private EventChannelInt dayChange;
        [SerializeField] private EventChannelInt hourChange;
        [SerializeField] private EventChannelInt minChange;

        [SerializeField] private EventChannel OnTimerReset;

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
            

            if (gameRate <= 0)
            {
                Debug.LogError("Game rate can not be zero or negative");
            }

            currentDay_InGame = startDay;

            ResetTimer();
        }

        private void Start()
        {
            dayChange?.RaiseEvent(currentDay_InGame);
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            
            if (OnTimerReset != null)
            {
                OnTimerReset.OnEvent -= ResetTimer;
            }
        }

        int lastMinuteBroadcasted;
        int lastHourBroadcasted;
        
        //int lastDayBroadcasted;
        private void Update()
        {
            //Convert current real time to game time
            currentTime_InGame += Time.deltaTime * gameRate;

            currentMin_InGame = (int) currentTime_InGame / 60;
            currentHour_InGame = (int)currentTime_InGame / 3600;

            //Checks to see if any events should be raised

            if (currentMin_InGame != lastMinuteBroadcasted)
            {
                minChange.RaiseEvent( currentMin_InGame );
                lastMinuteBroadcasted = currentMin_InGame;
            }

            if (currentHour_InGame != lastHourBroadcasted)
            {
                hourChange.RaiseEvent(currentHour_InGame);
                lastHourBroadcasted = currentHour_InGame;
            }

            //next day
            if (((int)currentTime_InGame / 86400 ) > 0 )
            {
                currentDay_InGame++;
                dayChange.RaiseEvent(currentDay_InGame);
                ResetTimer();
            }

            //if (currentDay_InGame != lastDayBroadcasted)
            //{
            //    dayChange.RaiseEvent(currentDay_InGame);
            //    lastDayBroadcasted = currentDay_InGame;
            //}


        }

        #endregion

        #region Methods
        
        //Pauses Timer if the game is paused
        private void OnGameStateChanged(GameState newGameState)
        {
            enabled = newGameState == GameState.Gameplay;
        }
        
        //reset
        private void ResetTimer()
        {
            //startingTime = (int)(((startHour * 3600) + (startMin * 60))/60 );
            currentTime_InGame = (((startHour * 3600) + (startMin * 60)) );
        }

        #endregion
    }
}