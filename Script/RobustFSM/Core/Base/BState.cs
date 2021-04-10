#define Verbose

using RobustFSM.Interfaces;
using System;

namespace RobustFSM.Base
{
    [Serializable]
    public class BState : IState
    {
        #region Propeties

        /// <summary>
        /// A reference to the name of this instance
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// A reference to the state machine that this instance belongs to
        /// </summary>
        public IFSM Machine { get; set; }

        /// <summary>
        /// A reference to the super machine that all sub statemachines belong to
        /// </summary>
        public IFSM SuperMachine { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrievs the state machine this instance belongs to
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <returns>the state machine</returns>
        public T GetMachine<T>() where T : IFSM
        {
            return (T)Machine;
        }

        /// <summary>
        /// Retrievs the super state machine this instance belongs to
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <returns>the state machine</returns>
        public T GetSuperMachine<T>() where T : IFSM
        {
            return (T)SuperMachine;
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Is called every time this state is activated
        /// </summary>
        public virtual void Enter()
        {
#if Verbose
            Console.WriteLine(Machine.MachineName + ":Enter(" + StateName + ")");
#endif
        }

        /// <summary>
        /// Is called every frame update
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// Is called every time this state is deactivated
        /// </summary>
        public virtual void Exit()
        {
#if Verbose
            Console.WriteLine(Machine.MachineName + ":Exit(" + StateName + ")");
#endif
        }

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public virtual void Initialize()
        {
            ///set state name
            if (string.IsNullOrEmpty(StateName))
                StateName = GetType().Name;
        }

        /// <summary>
        /// Is called on every manual execute in the state machine
        /// </summary>
        public virtual void ManualExecute() { }

        /// <summary>
        /// Is called on every physics execute in the state machine
        /// </summary>
        public virtual void PhysicsExecute() { }

        #endregion
    }
}