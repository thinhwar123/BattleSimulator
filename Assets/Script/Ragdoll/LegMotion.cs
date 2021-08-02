using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LegMotion : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Vector3 stopPoint;
    [SerializeField] private List<Vector3> pointMoveForwardList;
    [SerializeField] private List<Vector3> pointMoveBackwardList;
    private Tween tween;
    private void Start()
    {

    }
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
        int nextPointIndex = (pointIndex + 1) % pointMoveBackwardList.Count;
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
    public void WalkAStepForward()
    {
        StartCoroutine(CoWalkAStepForward());
    }
    public void WalkAStepBackward()
    {
        StartCoroutine(CoWalkAStepBackward());
    }
    public IEnumerator CoWalkAStepForward()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(pointMoveForwardList[0], time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        tween = transform.DOLocalMove(pointMoveForwardList[1], time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        tween = transform.DOLocalMove(stopPoint,time).SetEase(Ease.Linear);
    }
    public IEnumerator CoWalkAStepBackward()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(pointMoveBackwardList[0], time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        tween = transform.DOLocalMove(pointMoveBackwardList[1], time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        tween = transform.DOLocalMove(stopPoint, time).SetEase(Ease.Linear);
    }
}
