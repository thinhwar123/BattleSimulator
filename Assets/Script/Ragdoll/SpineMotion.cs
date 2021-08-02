using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpineMotion : MonoBehaviour
{
    [SerializeField] private float threshold;
    [SerializeField] private float time;
    //[SerializeField] private Transform centerPoint;
    [SerializeField] private Vector3 stopPoint;
    [SerializeField] private Vector3 pointMoveForward;
    [SerializeField] private Vector3 pointMoveBackward;
    private Tween tween;
    private void Start()
    {

    }
    public void MoveForward(Vector3 value)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(value, time).SetEase(Ease.Linear);
        //transform.localEulerAngles = new Vector3(15, 0, 0);
    }
    public void MoveBackward()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(pointMoveBackward, time).SetEase(Ease.Linear);
        //transform.localEulerAngles = new Vector3(-15, 0, 0);
    }
    public void StopMove()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOLocalMove(stopPoint, time).SetEase(Ease.Linear);
        transform.localEulerAngles = Vector3.zero;
    }
    public void SelfBalance(Transform tip, Transform centerPoint)
    {
 
    }
}
