using UnityEngine;

namespace RobustFSM.Interfaces
{
    /// <summary>
    /// Interface defination of a state
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// A refernce to the name of this instance
        /// </summary>
        string StateName { get; set; }

        /// <summary>
        /// A reference to the state machine that this instance belongs to
        /// </summary>
        IFSM Machine { get; set; }

        /// <summary>
        /// A reference to the super machine that all sub statemachines belong to
        /// </summary>
        IFSM SuperMachine { get; set; }

        /// <summary>
        /// Retrieves the state machine that this state belongs to
        /// </summary>
        /// <typeparam name="T">type of state</typeparam>
        /// <returns></returns>
        T GetMachine<T>() where T : IFSM;

        /// <summary>
        /// Retrieves the super state machine that this state belongs to
        /// </summary>
        /// <typeparam name="T">type of state</typeparam>
        /// <returns></returns>
        T GetSuperMachine<T>() where T : IFSM;

        /// <summary>
        /// Initializes this instance
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called once when a state is activated
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every frame execute
        /// </summary>
        void Execute();

        /// <summary>
        /// Called once when a state is deactivated
        /// </summary>
        void Exit();

        /// <summary>
        /// Called every custom execute
        /// </summary>
        void ManualExecute();

        /// <summary>
        /// Called every physics update
        /// </summary>
        void PhysicsExecute();
    }
}