using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using UnityEngine.Events;
using FirstGearGames.SmoothCameraShaker;

[RequireComponent(typeof(CharacterAbilityBehaviour))]
[RequireComponent(typeof(CharacterBrain))]
[DefaultExecutionOrder(-10000)]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterStatData stat;
    public CharacterBattleManager battleBehavior;
    [HideInInspector] public CharacterBrain brain;
    protected CharacterAbilityBehaviour abilityBehavior;
    public BattleEngager engager { get; private set; }

    [Header("Animation Related")]
    [SerializeField]
    private AnimancerComponent _Animancer;
    public AnimancerComponent Animancer => _Animancer;

    [SerializeField]
    private CharacterState.StateMachine _StateMachine;
    public CharacterState.StateMachine StateMachine => _StateMachine;
    public List<GenericAbility> abilities;

    [Header("Damage Text Settings")]
    public DynamicTextData damageTextData;
    public float textDistanceMaxOffset = 1;
    public Transform textSpawnOrigin = null;

    [Header("On Character Death")]
    public UnityEvent onDeath;

    [Header("Camera Shake On Hit")]
    public bool DoCameraShakeOnHit;
    public ShakeData cameraShakeOnHit;
    public ShakeData cameraShakeOnDeath;

    [Header("Aim Target")]
    public Transform target;

    private void Awake() {
        StateMachine.InitializeAfterDeserialize();
        engager = new BattleEngager(stat.regenStartTime);
        abilityBehavior = GetComponent<CharacterAbilityBehaviour>();
        battleBehavior = new CharacterBattleManager(this, abilityBehavior);
        for (int i = 0; i < abilities.Count; i++)
        {
            abilityBehavior.AddAbility(abilities[i]);
        }
        brain = GetComponent<CharacterBrain>();
        StateMachine.DefaultState = GetComponent<CharacterBrain>()._Idle;
        if (!target) target = GetComponent<Transform>();
        OnAwake();
    }

    public virtual void OnAwake() { }

    public virtual void OnDeath()
    {
        onDeath.Invoke();
    }

    public void StartCoroutineInCharacter(IEnumerator coroutineMethod){
        StartCoroutine(coroutineMethod);
    }

    public CharacterStatData GetStat(){
        return(this.stat);
    }
    public CharacterStatData SetStat(CharacterStatData x){
        this.stat = x;
        return(this.stat);
    }
    public CharacterStatData ResetBP()
    {
        this.stat = this.stat.SetBloodPoint(this.stat.max_bp);
        return (this.stat);
    }

    public void Terminate()
    {
        gameObject.layer = 2;
        StopAllCoroutines();
        abilityBehavior.StopAllCoroutines();
        abilityBehavior.enabled = false;
        GetComponent<CharacterController2D>().StopAllCoroutines();
        GetComponent<CharacterController2D>().enabled = false;
        brain.StopAllCoroutines();
        brain.enabled = false;
        this.enabled = false;
        if(gameObject.GetComponent<FlowerBoss>() == null)
        {
            //Debug.Log("Enemy");
            Destroy(gameObject, 2.5f);
        }
        else
        {
            //Debug.Log("Flower");
        }
    }
}
