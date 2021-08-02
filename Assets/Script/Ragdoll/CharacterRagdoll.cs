using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
    [SerializeField] private Transform com;
    [SerializeField] private Transform characterRagdoll;
    [SerializeField] private Transform characterAnimated;
    [SerializeField] private List<Transform> originTransformList;
    [SerializeField] private List<Transform> mirrorTransformList;

    private void Start()
    {
        Init();

    }

    private void Update()
    {
        //CopyMotion();
        com.position = CaculateCom();
    }

    public void Init()
    {
        Transform[] originTransforms = characterRagdoll.GetComponentsInChildren<Transform>();
        Transform[] mirrorTransforms = characterAnimated.GetComponentsInChildren<Transform>();
        originTransformList = new List<Transform>();
        mirrorTransformList = new List<Transform>();
        for (int i = 1; i < originTransforms.Length; i++)
        {
            if (originTransforms[i].TryGetComponent<ConfigurableJoint>(out ConfigurableJoint cj))
            {
                originTransformList.Add(originTransforms[i]);
                //mirrorTransformList.Add(mirrorTransforms[i]);
            }
        }
    }

    public Vector3 CaculateCom()
    {
        Vector3 a = Vector3.zero;
        float b = 0;
        for (int i = 0; i < originTransformList.Count; i++)
        {
            a += originTransformList[i].position * originTransformList[i].GetComponent<Rigidbody>().mass;
            b += originTransformList[i].GetComponent<Rigidbody>().mass;
        }
        return a / b;
    }
}
