using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using RobustFSM.Interfaces;
using UnityEditor;
using System.Linq;

namespace RobustFSM.Base
{
    /// <summary>
    /// Base finite state machine. This can be added to
    /// any class
    /// This is not an entity component
    /// You have to manually call the update functions from another script
    /// </summary>
    public class BFSM : IFSM
    {
        #region Variables

        /// <summary>
        /// A reference to the states contained by this instance
        /// </summary>
        public Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

        #endregion

        #region Properties

        /// <summary>
        /// A reference to the manual update frequency
        /// </summary>
        public float ManualUpdateFrequency { get; set; }

        /// <summary>
        /// A reference to the state name
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// A reference to the current state of this FSM
        /// </summary>
        public virtual IState CurrentState { get; set; }

        /// <summary>
        /// A reference to the initial state of this FSM
        /// </summary>
        public virtual IState InitialState { get; set; }

        /// <summary>
        /// A reference to the next state of this FSM
        /// </summary>
        public virtual IState NextState { get; set; }

        /// <summary>
        /// A reference to the previous state of this FSM
        /// </summary>
        public virtual IState PreviousState { get; set; }

        /// <summary>
        /// Property to set or get the states list
        /// </summary>
        public virtual Dictionary<Type, IState> States
        {
            get
            {
                return _states;
            }

            set
            {
                _states = value;
            }
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// REQUIRES IMPL
        /// Adds states to the machine with calls to AddState<>()
        ///     
        /// When all states have been added set the initial state with 
        /// a call toSetInitialState<>()
        /// </summary>
        public virtual void AddStates() { }

        /// <summary>
        /// Starts manual execution
        /// </summary>
        public virtual void StartManualExecute() { }

        /// <summary>
        /// Stops manual execution
        /// </summary>
        public virtual void StopManualExecute() { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Triggers the state machine to change state to the passed state
        /// </summary>
        /// <param name="type"></param>
        protected void ChangeState(Type type)
        {
            try
            {
                //cache the correct states
                PreviousState = CurrentState;
                NextState = States[type];

                //exit the current state
                CurrentState.Exit();
                CurrentState = NextState;
                NextState = null;

                //enter the current state
                CurrentState.Enter();
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception("\n" + GetType().Name + " could not be found in the state machine" + e.Message);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add the state to the FSM
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        public virtual void AddState<T>() where T : IState, new()
        {
            if (!ContainsState<T>())
            {
                //create new state 
                IState item = new T();
                item.SuperMachine = this;

                //add state to dictionary
                States.Add(typeof(T), item);
            }
        }

        /// <summary>
        /// Triggers a state transition of the FSM to the specified state
        /// </summary>
        /// <typeparam name="T">the next state</typeparam>
        public virtual void ChangeState<T>() where T : IState
        {
            ChangeState(typeof(T));
        }

        /// <summary>
        /// Checks whether this state machine contains the passed state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>true</c>, if state is available else <c>false</c></returns>
        public virtual bool ContainsState<T>() where T : IState
        {
            return States.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Returns the current state
        /// </summary>
        /// <typeparam name="T">the state type</typeparam>
        /// <returns>the current state</returns>
        public virtual T GetCurrentState<T>() where T : IState
        {
            return (T)CurrentState;
        }

        /// <summary>
        /// Returns the initial state
        /// </summary>
        /// <typeparam name="T">the state type</typeparam>
        /// <returns>the current state</returns>
        public virtual T GetInitialState<T>() where T : IState
        {
            return (T)InitialState;
        }

        /// <summary>
        /// Returns the previous state
        /// </summary>
        /// <typeparam name="T">the state type</typeparam>
        /// <returns>the previous state</returns>
        public virtual T GetPreviousState<T>() where T : IState
        {
            return (T)PreviousState;
        }

        /// <summary>
        /// Retrieves the specified state from the FSM
        /// </summary>
        /// <typeparam name="T">the state type</typeparam>
        /// <returns>the previous state</returns>
        public virtual T GetState<T>() where T : IState
        {
            return (T)States[typeof(T)];
        }

        /// <summary>
        /// Retrieves the list of all the states in this state machine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> GetStates<T>() where T : IState
        {
            return States.Values.Cast<T>().ToList();
        }

        /// <summary>
        /// Triggers the state machine to go to the previously executed state
        /// </summary>
        public virtual void GoToPreviousState()
        {
            ChangeState(PreviousState.GetType());
        }

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public virtual void Initialize()
        {
            //if no name hase been specified set the name of this instance to the the
            if (String.IsNullOrEmpty(MachineName))
                MachineName = GetType().Name;

            //add the states
            AddStates();

            //set the current state to be the initial state
            CurrentState = InitialState;

            //throw an error if we do not have an initial state
            if (CurrentState == null)
                throw new Exception("\n" + GetType().Name + "Initial state not specified.\tSpecify the initial state inside the AddStates()!!!\n");

            //initialize every state
            foreach (KeyValuePair<Type, IState> pair in States)
            {
                //set the super machine and initialize the state
                pair.Value.Machine = this;
                pair.Value.SuperMachine = this;
                pair.Value.Initialize();
            }

            //change to the current state
            CurrentState.Enter();
        }

        /// <summary>
        /// Sets the initial state as the current state
        /// </summary>
        /// <remarks>
        /// the state machine will ignore the exit method of the currently running
        /// state and the enter method of the new state
        /// </remarks>
        public void SetInitialStateToCurrentState()
        {
            CurrentState = InitialState;
        }

        /// <summary>
        /// Checks whether this state machine the current state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>true</c>, if state is the current state else <c>false</c></returns>
        public bool IsCurrentState<T>() where T : IState
        {
            return (CurrentState.GetType() == typeof(T)) ? true : false;
        }

        /// <summary>
        /// Checks whether this state machine the previous state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>true</c>, if state is the previous state else <c>false</c></returns>
        public bool IsPreviousState<T>() where T : IState
        {
            return (PreviousState.GetType() == typeof(T)) ? true : false;
        }

        /// <summary>
        /// Called every frame update
        /// </summary>
        /// <param name="frameTime"></param>
        public virtual void OnExecute()
        {
            //run execute
            if (CurrentState != null)
                CurrentState.Execute();
        }

        /// <summary>
        /// Called every custom update
        /// </summary>
        public virtual void OnManualExecute()
        {
            //run manual execute
            if (CurrentState != null)
                CurrentState.ManualExecute();
        }

        /// <summary>
        /// Called every physics update
        /// </summary>
        public virtual void OnPhysicsExecute()
        {
            //run physics execute
            if (CurrentState != null)
                CurrentState.PhysicsExecute();
        }

        /// <summary>
        /// Removes all states in the FSM
        /// </summary>
        public void RemoveAllStates()
        {
            States.Clear();
        }

        /// <summary>
        /// Removes a particular state from this state machine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void RemoveState<T>() where T : IState
        {
            //create the instance
            Type t = typeof(T);

            //remove instance in dictionary
            if (States.ContainsKey(t))
                States.Remove(t);
        }

        /// <summary>
        /// Sets the current state
        /// </summary>
        /// <typeparam name="T">the state</typeparam>
        public virtual void SetCurrentState<T>() where T : IState
        {
            CurrentState = States[typeof(T)];
        }

        /// <summary>
        /// Sets the initial state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void SetInitialState<T>() where T : IState
        {
            InitialState = States[typeof(T)];
        }

        #endregion
    }
}