using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HandMotion : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Vector3 stopPoint;
    [SerializeField] private List<Vector3> pointMoveForwardList;
    [SerializeField] private List<Vector3> pointMoveBackwardList;
    [SerializeField] private Vector3 chargeAttackPoint;
    private Tween tween;

    public void MoveForward(int pointIndex)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        int nextPointIndex = (pointIndex + 1) % pointMoveForwardList.Count;
        tween = transform.DOLocalMove(pointMoveForwardList[pointIndex], time).SetEase(Ease.Linear).OnComplete(() => MoveForward(nextPointIndex));
    }
    public void MoveBackward(int pointIndex)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        int nextPointIndex = (pointIndex + pointMoveBackwardList.Count - 1) % pointMoveBackwardList.Count;
        tween = transform.DOLocalMove(pointMoveBackwardList[pointIndex], time).SetEase(Ease.Linear).OnComplete(() => MoveBackward(nextPointIndex));
    }
    public void StopMove()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(stopPoint, time).SetEase(Ease.Linear);
    }
    public void ChargeAttack(float chargeTime)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(chargeAttackPoint, chargeTime).SetEase(Ease.Linear);
    }
}
