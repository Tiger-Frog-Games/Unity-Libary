using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StateMachine
    {
        private class Transition
        {
            public Func<bool> Condition { get; private set; }
            public IState To { get; private set; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        #region Variables

        private IState currentState;

        private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>();


        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public void tick()
        {
            Transition transition = GetTransition();

            if (transition != null) SetState(transition.To);

            currentState?.Tick();

        }

        public void SetState(IState state)
        {
            if (currentState == state)
            {
                return;
            }

            currentState?.OnExit();
            currentState = state;

            transitions.TryGetValue(currentState.GetType(), out currentTransitions);

            if(currentTransitions == null)
            {
                currentTransitions = EmptyTransitions;
            }

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (transitions.TryGetValue(from.GetType(), out List<Transition> transitionsFound) ==  false )
            {
                transitionsFound = new List<Transition>();
                transitions[from.GetType()] = transitionsFound;
            }

            transitionsFound.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (Transition transitions in anyTransitions)
            {
                if (transitions.Condition())
                {
                    return transitions;
                }
            }

            foreach(Transition transition in currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        } 

        #endregion
    }
}