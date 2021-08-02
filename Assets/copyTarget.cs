using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class copyTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 startLocalRotation;
    private void Start()
    {
        startLocalRotation = target.transform.localEulerAngles;
    }
    private void Update()
    {
        //startRotation = target.parent.eulerAngles;
        transform.localEulerAngles = target.parent.eulerAngles + startLocalRotation;
    }
    
}
