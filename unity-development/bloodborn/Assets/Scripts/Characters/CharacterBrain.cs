using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.FSM;

[RequireComponent(typeof(Character))]
public class CharacterBrain : MonoBehaviour
{
    protected Character _Character;

    [SerializeField] private bool autoDetectAnimation = true;

    #region Character States
    public CharacterState _Idle;
    [SerializeField] protected CharacterState _Move;
    [SerializeField] protected CharacterState _Fall;
    [SerializeField] private CharacterState _Wall;
    [SerializeField] private CharacterState _Ladder;

    [SerializeField] private CharacterState _Jump;
    [SerializeField] private CharacterState _Dash;
    [SerializeField] private CharacterState _Land;

    public CharacterState _Death;

    [SerializeField] private UseAbilityState _Ability;
    #endregion

    #region Animation Variables
    [HideInInspector] public float hSpeed;
    [HideInInspector] public float vSpeed;
    [HideInInspector] public float exSpeed;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public bool isOnWall;
    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool isOnLadder;
    [HideInInspector] public bool isInvulnerable;

    public void SetBool(string _name, bool _value)
    {
        switch (_name)
        {
            case "grounded":
                isGrounded = _value;
                break;
            case "dashing":
                isDashing = _value;
                break;
            case "onWall":
                isOnWall = _value;
                break;
            case "facingRight":
                isFacingRight = _value;
                break;
            case "onLadder":
                isOnLadder = _value;
                break;
            case "invulnerable":
                isInvulnerable = _value;
                break;
        }
    }

    public void SetFloat(string _name, float _value)
    {
        switch (_name)
        {
            case "hSpeed":
                hSpeed = _value;
                break;
            case "vSpeed":
                vSpeed = _value;
                break;
            case "exSpeed":
                exSpeed = _value;
                break;
        }
    }
    #endregion

    private void Start()
    {
        _Character = GetComponent<Character>();
        onStart();
    }

    public virtual void onStart() { }

    private void Update()
    {
        if (autoDetectAnimation)
        {
            UpdateMovement();
            UpdateAction();
        }
    }

    private void UpdateMovement()
    {
        if (_Character.StateMachine.CurrentState == _Fall && isGrounded && hSpeed == 0f)
        {
            _Character.StateMachine.TrySetState(_Land);
        }

        if (isDashing)
        {
            _Character.StateMachine.TrySetState(_Dash);
        }
        else if (hSpeed != 0 && isGrounded)
        {
            _Character.StateMachine.TrySetState(_Move);
        }
        else if (isOnLadder)
        {
            _Character.StateMachine.TrySetState(_Ladder);
        }
        else if (hSpeed == 0 && isGrounded)
        {
            _Character.StateMachine.TrySetState(_Idle);
        }
        else if (isOnWall)
        {
            _Character.StateMachine.TrySetState(_Wall);
        }
        else if (vSpeed < 0)
        {
            _Character.StateMachine.TrySetState(_Fall);
        }
    }

    private void UpdateAction()
    {
    }

    public void Jump()
    {
        _Character.StateMachine.TryResetState(_Jump);
    }

    public void Die()
    {
        if (_Character.gameObject != Player.player.gameObject)
        {
            //Debug.Log("Enemy");
            _Character.StateMachine.ForceSetState(_Death);
            _Character.Terminate();
        }
        else
        {
            //Debug.Log("Player");
        }
    }

    public void UseAbility(GenericAbility ability)
    {
        _Ability.clip = ability.clip;
        _Character.StateMachine.TrySetState(_Ability);
    }
}
