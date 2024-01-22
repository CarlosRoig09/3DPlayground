# 3DPlayground
A survival 3D project I made during my High technician studies. This project doesn't follow a purpose as a Video Game, is only a testing of the 3D environment on Unity. 

In this project I made use of the new Input System, a cinemachineFreeLook who follows the player, can be controlled by the cinemachine input provider script and a cinemachine collider (to control the camera behavior when an object is behind the player), a cinemachine virtual camera for cinematic scenes, the use of terrain and character 3d controller. 

On a programming level, this is the first project I tested some new concepts:

- Behavior Tree made with Scriptable Objects.

-Nav mesh agent.

- Usable object to activate an event (Key to Door): I use an interface "IWaitTillUsableItem" and a script called UsableItemBehaivour. Both share an ID. The Game Object with the UsableItemBehaivour when is collected by the player, it notifies the Singleton Game Manager and search which IWaitTillUsableItem share the same ID.  When the method finds it, it subscribes the method from IWaitTillUsableItem to the event of the UsableItemBehaivour. 

- Manage Input System subscription: Two different methods who subscribe or cancel the subscription of the actions from the Input System by the use of Enums. 
