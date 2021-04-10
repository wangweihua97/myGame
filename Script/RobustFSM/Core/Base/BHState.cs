using System;
using System.Collections.Generic;
using RobustFSM.Interfaces;
using Assets.RobustFSM.Interfaces;

namespace RobustFSM.Base
{
    /// <summary>
    /// This is a basse hybrid state. It belongs to a state machine.
    /// It allows your class to act as both a state machine and a state
    /// Inherit from this class if you want to create a hybrid state
    /// and override the appropriate methods
    /// </summary>
    public class BHState : BState, IHState
    {
        #region Variables

        /// <summary>
        /// A reference to the fsm of this instance
        /// </summary>
        private IFSM _fsm = new BFSM();

        #endregion

        #region Properties

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
        /// Triggers the state machine to go to the previously executed state
        /// </summary>
        public void GoToPreviousState()
        {
            _fsm.GoToPreviousState();
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

        #region Overriden Methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            //execute base
            base.Initialize();
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

            //Change the state machine for this instance
            foreach (KeyValuePair<Type, IState> pair in States)
            {
                //set the machine to be this instance
                pair.Value.Machine = this;

                //the super machine is the one specified for this instance
                pair.Value.SuperMachine = SuperMachine;

                //initialize the state
                pair.Value.Initialize();
            }
        }

        /// <summary>
        /// Executed every time this state is activated
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            //set the initial state
            _fsm.SetInitialStateToCurrentState();
            _fsm.CurrentState.Enter();
        }

        /// <summary>
        /// Called everytime this state is deactivated
        /// </summary>
        public override void Exit()
        {
            //exit the current state
            _fsm.CurrentState.Exit();

            //exit this state
            base.Exit();

        }

        /// <summary>
        /// Called every frame execute
        /// </summary>
        /// <param name="frameTime">time between updates</param>
        public override void Execute()
        {
            base.Execute();

            //call the execute of the child state
            OnExecute();
        }

        /// <summary>
        /// Called every manual execute
        /// </summary>
        public override void ManualExecute()
        {
            base.ManualExecute();

            //call the manual execute of the child state
            OnManualExecute();
        }

        /// <summary>
        /// Called every physics execute
        /// </summary>
        /// <param name="frameTime">time between updates</param>
        public override void PhysicsExecute()
        {
            base.PhysicsExecute();

            //call the physics update of the child state
            OnPhysicsExecute();
        }

        #endregion

        #region Unimplemented Methods

        /// <summary>
        /// Starts the manual execute
        /// </summary>
        /// <remarks>
        /// </remarks>
        public void StartManualExecute() { }

        /// <summary>
        /// Stops manual execution
        /// </summary>
        public void StopManualExecute() { }

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

        #endregion

    }
}