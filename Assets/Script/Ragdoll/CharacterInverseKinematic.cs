using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterInverseKinematic : MonoBehaviour
{
    [Header("CheckValue")]
    [SerializeField] private bool selfBalance;
    [SerializeField] private float threshold;
    [SerializeField] private float maxLegDistance;
    [Header("Center Point")]
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform characterRagdoll;    
    [SerializeField] private Rigidbody[] rigidbodieList;
    [SerializeField] private Rigidbody hip;
    [Header("Spine")]
    [SerializeField] private SpineMotion spineMotion;
    [SerializeField] private List<ConfigurableJoint> spine;
    [Header("Left Leg")]
    [SerializeField] private Transform leftLegPoint;
    [SerializeField] private LegMotion leftLegMotion;
    [SerializeField] private List<ConfigurableJoint> leftLeg;
    [Header("Right Leg")]
    [SerializeField] private Transform rightLegPoint;
    [SerializeField] private LegMotion rightLegMotion;
    [SerializeField] private List<ConfigurableJoint> rightLeg;
    [Header("Left Hand")]
    [SerializeField] private HandMotion leftHandMotion;
    [SerializeField] private List<ConfigurableJoint> leftHand;
    [Header("Right Hand")]
    [SerializeField] private HandMotion rightHandMotion;
    [SerializeField] private List<ConfigurableJoint> rightHand;

    private JointDrive freeJoin;
    private Coroutine balanceCoroutine;
    private UnityAction balanceFunctionCallback;
    private void Start()
    {
        freeJoin = new JointDrive();
        freeJoin.positionDamper = 0;
        freeJoin.positionDamper = 0;
        freeJoin.maximumForce = Mathf.Infinity;

        rigidbodieList = characterRagdoll.GetComponentsInChildren<Rigidbody>();
        balanceFunctionCallback = WalkAStepForward;
    }
    private void Update()
    {
        CalculatePoint();
        //Balance();
        if (Input.GetKeyDown(KeyCode.I))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            MoveBackward();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopMove();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            hip.AddForce(-hip.velocity * 10, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            WalkAStepBackward();
        }
    }
    public void Balance()
    {
        //if (!selfBalance)
        //{
        //    if (centerPoint.localPosition.z - threshold > leftLegPoint.localPosition.z && centerPoint.localPosition.z - threshold > rightLegPoint.localPosition.z)
        //    {
        //        balanceCoroutine = StartCoroutine(SelfBalance(WalkAStepForward));
        //    }
        //    else if (centerPoint.localPosition.z + threshold < leftLegPoint.localPosition.z && centerPoint.localPosition.z + threshold < rightLegPoint.localPosition.z)
        //    {
        //        balanceCoroutine = StartCoroutine(SelfBalance(WalkAStepBackward));
        //    }
        //    else if (Mathf.Abs((leftLegPoint.localPosition - rightLegPoint.localPosition).z) > maxLegDistance)
        //    {
        //        balanceCoroutine = StartCoroutine(SelfBalance(balanceFunctionCallback));
        //    }
        //}
        //if (!selfBalance)
        //{
        //    if (centerPoint.localPosition.z - threshold > leftLegPoint.localPosition.z && centerPoint.localPosition.z - threshold > rightLegPoint.localPosition.z)
        //    {
        //        spineMotion.MoveBackward();
        //    }
        //    else if (centerPoint.localPosition.z + threshold < leftLegPoint.localPosition.z && centerPoint.localPosition.z + threshold < rightLegPoint.localPosition.z)
        //    {
        //        spineMotion.MoveForward(new Vector3(0, 1.2f, 1));
        //    }
        //}
    }
    internal void StopBalance()
    {
        if (balanceCoroutine != null)
        {
            leftLegMotion.StopAllCoroutines();
            rightLegMotion.StopAllCoroutines();
            StopCoroutine(balanceCoroutine);
        }
    }
    public void CalculatePoint()
    {
        Vector3 a = Vector3.zero;
        float b = 0;
        for (int i = 0; i < rigidbodieList.Length; i++)
        {
            a += rigidbodieList[i].position * rigidbodieList[i].GetComponent<Rigidbody>().mass;
            b += rigidbodieList[i].GetComponent<Rigidbody>().mass;
        }
        centerPoint.position = a / b;

        leftLegPoint.position = leftLeg[0].transform.position;
        rightLegPoint.position = rightLeg[0].transform.position;
    }
    public void MoveForward()
    {
        //TODO: fix cung 2 diem di chuyen 0 - 2
        balanceFunctionCallback = WalkAStepForward;

        if (leftLegPoint.localPosition.z > rightLegPoint.localPosition.z)
        {
            leftLegMotion.MoveForward(2);
            rightLegMotion.MoveForward(0);
        }
        else
        {
            leftLegMotion.MoveForward(0);
            rightLegMotion.MoveForward(2);

        }
        //spineMotion.MoveForward(new Vector3(0, 1.2f, 1));
    }
    public void MoveBackward()
    {
        balanceFunctionCallback = WalkAStepBackward;

        if (leftLegPoint.localPosition.z > rightLegPoint.localPosition.z)
        {
            leftLegMotion.MoveBackward(0);
            rightLegMotion.MoveBackward(2);
        }
        else
        {
            leftLegMotion.MoveBackward(2);
            rightLegMotion.MoveBackward(0);
        }
        //spineMotion.MoveBackward();
    }
    public void StopMove()
    {
        selfBalance = false;
        leftLegMotion.StopMove();
        rightLegMotion.StopMove();
        spineMotion.StopMove();
    }
    public void WalkAStepForward()
    {
        if (leftLegPoint.localPosition.z < rightLegPoint.localPosition.z)
        {
            leftLegMotion.WalkAStepForward();
        }
        else
        {
            rightLegMotion.WalkAStepForward();
        }

    }
    public void WalkAStepBackward()
    {
        if (leftLegPoint.localPosition.z > rightLegPoint.localPosition.z)
        {
            leftLegMotion.WalkAStepBackward();
        }
        else
        {
            rightLegMotion.WalkAStepBackward();
        }
    }
    IEnumerator Attack(float chargeTime)
    {
        rightHandMotion.ChargeAttack(chargeTime);
        yield return new WaitForSeconds(chargeTime* 2);
        List<JointDrive> driveList = new List<JointDrive>();
        for (int i = 1; i < rightHand.Count; i++)
        {
            JointDrive tempDrive = new JointDrive();
            tempDrive.positionSpring = rightHand[i].slerpDrive.positionSpring;
            tempDrive.positionDamper = rightHand[i].slerpDrive.positionDamper;
            tempDrive.maximumForce = Mathf.Infinity;
            driveList.Add(tempDrive);

            rightHand[i].slerpDrive = freeJoin;
        }
        rightHand[0].GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
        yield return new WaitForSeconds(chargeTime);
        for (int i = 1; i < rightHand.Count; i++)
        {
            rightHand[i].slerpDrive = driveList[i-1];
        }
    }
    IEnumerator SelfBalance(UnityAction balanceFunction)
    {
        selfBalance = true;
        balanceFunction();
        yield return new WaitForSeconds(0.5f);
        while (Mathf.Abs((leftLegPoint.localPosition - rightLegPoint.localPosition).z) > maxLegDistance)
        {
            balanceFunction();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        selfBalance = false;
    }
}
