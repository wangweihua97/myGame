Welcome to RobustFSM v 2.0.0

RobustFSM is a package designed to ease the creation
of finite state machines for your project. RobustFSM is
ideal for simple finite state machines. Besides having the
ability to create finite state machines in the grasp of
hands, you can also create hierichal state machines from
RobustFSM
RobustFSM comes with a demo scene to show how you can implement this 
package.

What changed in version 2.0.0
-----------------------------
After studying y code for a while I realised I had to make my code more
readable, mantainable and broad. So I went into a research on how I can achieve this.
Besides being a unity plugin RobustFSM is more verstale now. 
You can now use it any C# project. All the core logic of RobustFS is now found 
in the Core folder of the package. The Mono folder contains the implementation of
RobustFSM for Unity 3D. If you feel RobustFSM is useful in some other project simply 
copy it to your project, delete the Mono project and implement your version of the FSM.
The other changes involves the renaming of some functions and variables

How to install
--------------
Simply the import the package into your project. I'm sure you have
done this already. You can move the RobustFSM folder into any
location of your convienience

How to use
-----------
To create a finite state machine, simply inherit from the finite
state machine base class as shown "MonoBFSM" e.g

'public class CustomFSM : MonoBFSM'

The MonoBFSM inherits from the MonoBehaviour class and the IFSM interface. You will need to
implement the AddStates method of the MonoBFSM. Look at the example 
in the demo scene

To create a state, inherit from the base state class(BState) e.g

'public class CustomState : BState'

If you want to create a hybrid state, instead of inheriting from
the BState class inherit form the base hybrid state machine class
(BHState). This allows your state to act both like a state and a state 
machine e.g

'public class CustomHeirichalState : BHState'

To access the super state machine inside a state
use this logic

CharacterFSM OwnerFSM
{
	return
	{
		get
		{
			(CharacterFSM)SuperMachine;
		}
	}
}

To access the game object that the super state machine is applied to
use this logic

public Character Owner
{
    get
    {
        return OwnerFSM.Owner;
    }
}

When inside a hybrid state, to access the owner of a state
use this logic

[statename]
IdleMainState OwnerFSM
{
	return
	{
		get
		{
			(IdleMainState)Machine;
		}
	}
}

Check the demo scene of more examples to access the owner finite state machine

To change state simply use this logic

'machine.ChangeState<NextState>();'

This applies for both the basic state and the hybrid state. If you are in a
BHState and you want to trigger a state transition of that state use

'ChangeState<NextState>();

Thus

'machine.ChangeState<Next>()' - will trigger the FSM owning this state to go to the next state
'ChangeState<NextState>()' - works if the calling state inherits from the BHState, will trigger a state
transition to the net state in that sub state machine.

Migrating from v1.0.0 to v2.0.0
-Your FSM now inherits from MonoFSM
-Any reference to MonoBFSM needs to be changed to MonoFSM

Thank you for using RobustFSM. You are free to add features to this package and if you do
please share them so that I also share with the rest of the world.

Here is some of my assets
Intelligent Selector - https://www.assetstore.unity3d.com/#!/content/125927
Soccer AI - https://assetstore.unity.com/packages/templates/soccer-ai-63743

Contact
Developer: Andrew Blessing Manyore
Company: Wasu Studio
Email: andyblem@gmail.com		
Mobile: +263733888022