using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumLibrary;

public class AnimationController : MonoBehaviour
{
    private Animator _anim;
    private bool _animationTransition;
    public bool AnimTransition
    {
        get { return _animationTransition; }
    }
    private float _desierweight;
    private float _currentWeight;
    private int _currentIndex;
    private float _multiplicator;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _anim);
        _animationTransition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_animationTransition)
        {
            _anim.SetLayerWeight(_currentIndex, _currentWeight);
            if (_desierweight != _currentWeight)
            {
                _currentWeight += Time.deltaTime * _multiplicator;
                if (_currentWeight < 0)
                    _currentWeight = 0;
                if (_currentWeight > 1)
                    _currentWeight = 1;
            }
            else
                _animationTransition = false;
        }
    }

    public void SetAnimationLayerWeight(int index, float weight, float speed)
    {
        _currentIndex = index;
        _desierweight = weight;
        _currentWeight = _anim.GetLayerWeight(index);
        _animationTransition = true;
        if (_desierweight > _currentWeight)
            _multiplicator = 1;
        else
            _multiplicator = -1;
        _multiplicator *= speed;
    }

    public void AnimationAction(AnimationActions[] actions, object[] parameters)
    {
        foreach (var action in actions)
        {
            switch (action)
            {
                case AnimationActions.Attack:
                    _anim.SetBool("Attack", true);
                    break;
                case AnimationActions.Countdown:
                    _anim.SetBool("Attack", false);
                    break;
                case AnimationActions.Walk:
                    _anim.SetFloat("MovementX", Mathf.Abs((float)parameters[0]));
                    _anim.SetFloat("MovementZ", Mathf.Abs((float)parameters[1]));
                    break;
                case AnimationActions.Jump:
                    _anim.SetBool("Jump", (bool)parameters[0]);
                    break;
                case AnimationActions.Idle:
                    break;
                case AnimationActions.Crouch:
                    break;
            }
        }
    }
}
