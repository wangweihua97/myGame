using RobustFSM.Base;
using RobustFSM.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.RobustFSM.Mono
{
    public class MonoFSM : MonoBehaviour, IFSM
    {
        #region Variables

        /// <summary>
        /// A reference to the fsm of this instance
        /// </summary>
        private IFSM _fsm = new BFSM();

        #endregion

        #region Properties

        /// <summary>
        /// Property to access the manual update coroutine
        /// </summary>
        public Coroutine ManualCoroutine { get; set; }

        /// <summary>
        /// A reference to the manual update frequency of the state machine
        /// </summary>
        public float ManualUpdateFrequency
        {
            get
            {
                return _fsm.ManualUpdateFrequency;
            }

            set
            {
                _fsm.ManualUpdateFrequency = value;
            }
        }

        /// <summary>
        /// A reference to the machine name
        /// </summary>
        public string MachineName
        {
            get
            {
                return _fsm.MachineName;
            }

            set
            {
                _fsm.MachineName = value;
            }
        }

        /// <summary>
        /// A reference to the current state
        /// </summary>
        public IState CurrentState
        {
            get
            {
                return _fsm.CurrentState;
            }

            set
            {
                _fsm.CurrentState = value;
            }
        }

        /// <summary>
        /// A reference to the inital state
        /// </summary>
        public IState InitialState
        {
            get
            {
                return _fsm.InitialState;
            }

            set
            {
                _fsm.InitialState = value;
            }
        }

        /// <summary>
        /// A reference to the next state
        /// </summary>
        public IState NextState
        {
            get
            {
                return _fsm.NextState;
            }

            set
            {
                _fsm.NextState = value;
            }
        }

        /// <summary>
        /// A reference to the previous state
        /// </summary>
        public IState PreviousState
        {
            get
            {
                return _fsm.PreviousState;
            }

            set
            {
                _fsm.PreviousState = value;
            }
        }

        /// <summary>
        /// A reference to the states contained by this instance
        /// </summary>
        public virtual Dictionary<Type, IState> States
        {
            get
            {
                return _fsm.States;
            }

            set
            {
                _fsm.States = value;
            }
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// REQUIRES IMPL
        /// Adds states to the machine with calls to AddState<>()
        ///     
        /// When all states have been added set the initial state with 
        /// a call toSetInitialState<>()
        /// </summary>
        public virtual void AddStates() { }


        /// <summary>
        /// Starts the manual execute
        /// </summary>
        /// <remarks>
        /// </remarks>
        public void StartManualExecute()
        {
            //start the custom update coroutine if valid
            if (ManualUpdateFrequency > 0)
                ManualCoroutine = StartCoroutine(ManualExecute());
        }

        /// <summary>
        /// Stops the manual execute
        /// </summary>
        /// <remarks>
        /// </remarks>
        public void StopManualExecute()
        {
            //start the custom update coroutine if valid
            if (ManualCoroutine != null)
            {
                //stop the running coroutine
                StopCoroutine(ManualCoroutine);
                ManualCoroutine = null;
            }
        }

        #endregion

        #region Basic Methods

        /// <summary>
        /// Adds a state to this instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddState<T>() where T : IState, new()
        {
            //add the state
            _fsm.AddState<T>();
        }

        /// <summary>
        /// Triggers a state change in this instance
        /// </summary>
        /// <typeparam name="T">state to change to</typeparam>
        public void ChangeState<T>() where T : IState
        {
            _fsm.ChangeState<T>();
        }

        /// <summary>
        /// Checks whether this instance contains a particular state
        /// </summary>
        /// <typeparam name="T">state to check</typeparam>
        /// <returns><c>true</c>, if state is available else <c>false</c></returns>
        public bool ContainsState<T>() where T : IState
        {
            return _fsm.ContainsState<T>();
        }

        /// <summary>
        /// Retrieves the current executing state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>the state</c>, if state is available else <c>null</c></returns>
        public T GetCurrentState<T>() where T : IState
        {
            return _fsm.GetCurrentState<T>();
        }

        /// <summary>
        /// Returns the initial state
        /// </summary>
        /// <typeparam name="T">the state type</typeparam>
        /// <returns>the current state</returns>
        public T GetInitialState<T>() where T : IState
        {
            return _fsm.GetInitialState<T>();
        }

        /// <summary>
        /// Retrieves the previous executed state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>the state</c>, if state is available else <c>null</c></returns>
        public T GetPreviousState<T>() where T : IState
        {
            return _fsm.GetPreviousState<T>();
        }

        /// <summary>
        /// Retrieves a particular state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>the state</c>, if state is available else <c>null</c></returns>
        public T GetState<T>() where T : IState
        {
            return _fsm.GetState<T>();
        }

        /// <summary>
        /// Retrieves the list of states contained by this instance
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>the state list</c> else <c>null</c></returns>
        public List<T> GetStates<T>() where T : IState
        {
            return _fsm.GetStates<T>();
        }

        /// <summary>
        /// Checks whether this state machine the current state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>true</c>, if state is the current state else <c>false</c></returns>
        public bool IsCurrentState<T>() where T : IState
        {
            return _fsm.IsCurrentState<T>();
        }

        /// <summary>
        /// Checks whether this state machine the previous state
        /// </summary>
        /// <typeparam name="T">state type</typeparam>
        /// <returns><c>true</c>, if state is the previous state else <c>false</c></returns>
        public bool IsPreviousState<T>() where T : IState
        {
            return _fsm.IsPreviousState<T>();
        }

        /// <summary>
        /// Removes a particular state from this state machine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveState<T>() where T : IState
        {
            _fsm.RemoveState<T>();
        }

        /// <summary>
        /// Sets the current state
        /// </summary>
        /// <typeparam name="T">the state</typeparam>
        public void SetCurrentState<T>() where T : IState
        {
            _fsm.SetCurrentState<T>();
        }

        /// <summary>
        /// Sets the initial state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetInitialState<T>() where T : IState
        {
            _fsm.SetInitialState<T>();
        }

        /// <summary>
        /// Sets the custome update frequency of the FSM
        /// </summary>
        /// <param name="value"></param>
        public void SetUpdateFrequency(float value)
        {
            //update the manual execute
            ManualUpdateFrequency = value;

            //start manual execute if the coroutine is not running
            if (ManualCoroutine == null)
                StartManualExecute();
        }

        /// <summary>
        /// Triggers the state machine to go to the previously executed state
        /// </summary>
        public void GoToPreviousState()
        {
            _fsm.GoToPreviousState();
        }

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public void Initialize()
        {
            //if no name hase been specified set the name of this instance to the the
            if (string.IsNullOrEmpty(MachineName))
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

            //start the manual execute
            StartManualExecute();
        }

        /// <summary>
        /// Called every frame update
        /// </summary>
        public void OnExecute()
        {
            _fsm.OnExecute();
        }

        /// <summary>
        /// Called every custom update
        /// </summary>
        public void OnManualExecute()
        {
            _fsm.OnManualExecute();
        }

        /// <summary>
        /// Called every physics update
        /// </summary>
        public void OnPhysicsExecute()
        {
            _fsm.OnPhysicsExecute();
        }

        /// <summary>
        /// Removes all states in the FSM
        /// </summary>
        public void RemoveAllStates()
        {
            _fsm.RemoveAllStates();
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
            _fsm.SetInitialStateToCurrentState();
        }

        #endregion

        #region Monobehavior Methods

        /// <summary>
        /// Called at the start of the game.
        /// </summary>
        protected void Start()
        {
            //initialize this instance
            Initialize();
        }

        /// <summary>
        /// Called every custom update
        /// </summary>
        protected IEnumerator ManualExecute()
        {
            while (true)
            {
                //run the manual execute
                OnManualExecute();

                //wait for the specified time
                yield return new WaitForSeconds(ManualUpdateFrequency);
            }
        }

        /// <summary>
        /// Draws a gizmo on the player
        /// </summary>
        void OnDrawGizmos()
        {
            if (CurrentState != null)
            {
                //set the print text
                string printText = MachineName + "\n" + CurrentState.StateName;

                //render the label
                Handles.Label(transform.position, printText);
            }
        }

        /// <summary>
        /// Called everytime time just before the physics updates
        /// </summary>
        /// <param name="frameTime"></param>
        protected void FixedUpdate()
        {
            //run the physics execute
            OnPhysicsExecute();
        }

        /// <summary>
        /// Called once every frame when the game is running.
        /// </summary>
        protected void Update()
        {
            //run the execute
            OnExecute();
        }

        #endregion
    }
}
