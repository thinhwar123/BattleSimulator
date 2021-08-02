using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Character Data

    [Header("CharacterData")]
    [SerializeField] private CharacterData characterBaseData;
    [SerializeField] private CharacterIdleStateData characterIdleStateData;
    [SerializeField] private CharacterMoveStateData characterMoveStateData;
    [SerializeField] private CharacterPunchStateData CharacterPunchStateData;
    #endregion

    #region States
    public FiniteStateMachine stateMachine { get; private set; }

    public CharacterIdleState characterIdleState { get; private set; }
    public CharacterMoveState characterMoveState { get; private set; }
    public CharacterPunchState characterPunchState { get; private set; }

    #endregion

    #region Components
    [Header("Components")]
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    
    public CharacterInverseKinematic characerIK;

    #endregion

    #region CharacterAtrribute
    [Header("CharacterAtrribute")]
    [SerializeField] private int hitpoint;

    #endregion

    #region CheckTranforms

    [Header("CheckTranform")]
    [SerializeField] private Transform emenyCheck;

    #endregion

    #region OtherValue

    [Header("OtherValue")]
    [SerializeField] private Transform characterRagdoll;
    [SerializeField] private Transform characterAnimated;
    [SerializeField] public bool canMove;
    #endregion


    #region Unity Function
    private void Awake()
    {
        stateMachine = new FiniteStateMachine();

        characterIdleState = new CharacterIdleState(this, characterIdleStateData);
        characterMoveState = new CharacterMoveState(this, characterMoveStateData);
        characterPunchState = new CharacterPunchState(this, CharacterPunchStateData);

        stateMachine.Initialize(characterIdleState);
    }
    private void Start()
    {

    }
    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region SetFunction
    public void SetTriggerAnimator (string animationString)
    {
        //animator.SetTrigger(animationString);
    }
    public void SetBoolAnimator(string animationString, bool value)
    {
        //animator.SetBool(animationString, value);
    }
    //public void SetCharacterInverseKinematic(CharacterMotion motion)
    //{
    //    switch (motion)
    //    {
    //        case CharacterMotion.STOPMOVE:
    //            characerIK.StopMove();
                
    //            break;
    //        case CharacterMotion.MOVEFORWARD:
    //            characerIK.MoveForward();
    //            break;
    //        case CharacterMotion.MOVEBACKWARD:
    //            characerIK.MoveBackward();
    //            break;
    //    }
    //}
    public void SetVelocity(Vector3 direction, float speed = 0)
    {
        //if (speed == 0)
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //}
        //else
        //{
        //    rb.velocity = new Vector3(direction.x * speed , rb.velocity.y, direction.z * speed );
        //}
    }
    public void SetRotate(Vector3 eulerAngles)
    {
        characterRagdoll.localEulerAngles = eulerAngles;
        characterAnimated.localEulerAngles = eulerAngles;
    }
    #endregion

    #region CheckFunctions
    public bool CheckIfEnemyAround(float rangeCheck)
    {
        return Physics.OverlapSphere(emenyCheck.position, rangeCheck).Length != 0;
    }
    #endregion

    #region OtherFunctions
    public void AnimationFinish()
    {
        stateMachine.currentState.AnimationFinish();
    }
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }
    public void ResetAnimatorTranform()
    {
        animator.transform.localPosition = Vector3.zero;
        animator.transform.localRotation = Quaternion.identity;
    }
    #endregion
}
 
public enum CharacterMotion 
{
    MOVEFORWARD,
    MOVEBACKWARD,
    STOPMOVE
}
