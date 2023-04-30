using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumLibrary;

public class WeaponData : ItemData
{
    //Type of weapon. Indicate if the weapon is range or melee one.
    public WeaponType Type;
    //Script WeaponController who have the GameObject weapon. No need to add a value en the ScriptableObject, the value is written in the DetachWeapon Script.
    public WeaponController Script;
    //Animator the weapon is going to use. Normally, this animator is the Player one.
    //public AnimationController Anim;
    //Weapon type of animation. This set wich animation layer the animator is going to have active.
    public WeaponAnimType AnimType;
    //This are the states the weapon can have. States have one action assigned, this states can transition between them. 
    //The order for the weapons has to be.
    //Idle
    //Countdown
    //Attack
    public ScriptableState[] States;
    //Duration of the attackAction and animation
    public float AttackDuration;
    //Time between the attack action and the idle one. While this time is not 0, the Player can't attack.
    public float Countdown;
    //Some states use this bools to indicate there is a current state active and another state can't transitate to it.
    public bool StateIsActive;
    //This indicate if, in the idle state, the weapon is subscribed to the attack and countdown states. If is not, then, it subscribe to this states.
    public bool EventSubscriber;
    public Vector3 WeaponPosition;
    public Quaternion WeaponRotation;
    public float Impulse;
}
