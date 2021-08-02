using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShowDebug : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 stopPoint;
    [SerializeField] private int startPoint;
    [SerializeField] private List<Vector3> pointList;
    private Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToPoint(startPoint, true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopMove();
        }
    }
    public void MoveToPoint(int pointIndex, bool isStart = false)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        if (isStart)
        {
            int nextPointIndex = (pointIndex + 1) % pointList.Count;
            tween = transform.DOLocalMove(pointList[pointIndex], 0.1f).SetEase(Ease.Linear).OnComplete(() => MoveToPoint(nextPointIndex));
        }
        else
        {
            int nextPointIndex = (pointIndex + 1) % pointList.Count;
            int lastPointIndex = (pointIndex + pointList.Count - 1) % pointList.Count;
            float time = Mathf.Round((pointList[lastPointIndex] - pointList[pointIndex]).magnitude / speed * 100) * 0.01f;
            tween = transform.DOLocalMove(pointList[pointIndex], time).SetEase(Ease.Linear).OnComplete(() => MoveToPoint(nextPointIndex));
        }


    }
    public void StopMove()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        float time = Mathf.Round((transform.localPosition - stopPoint).magnitude / speed * 100) * 0.01f;
        tween = transform.DOLocalMove(stopPoint, time).SetEase(Ease.Linear);
    }
}
