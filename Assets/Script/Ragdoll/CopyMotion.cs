using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    [SerializeField] private bool doNotSync;
    [SerializeField] private bool copyLocalPosition;
    [SerializeField] private bool copyLocalRotation;
    [SerializeField] private GameObject mirrorJoint;

    Rigidbody myRigidBody;
    ConfigurableJoint myJoint;

    //starting point (anchor for the joints)
    Vector3 MirrorAnchorPosition;
    Quaternion MirrorAnchorRotation;
    private void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
        myJoint = this.gameObject.GetComponent<ConfigurableJoint>();

        MirrorAnchorPosition = mirrorJoint.transform.localPosition;
        MirrorAnchorRotation = mirrorJoint.transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (!doNotSync)
        {
            Vector3 MirrorTargetPosition = GetTargetPosition(mirrorJoint.transform.localPosition, MirrorAnchorPosition);
            myJoint.targetPosition = MirrorTargetPosition;

            Quaternion MirrorTargetRotation = GetTargetRotation(mirrorJoint.transform.localRotation, MirrorAnchorRotation);
            myJoint.targetRotation = MirrorTargetRotation;

        }
        if (copyLocalPosition)
        {
            transform.localPosition = mirrorJoint.transform.localPosition;
        }
        if (copyLocalRotation)
        {
            transform.localRotation = mirrorJoint.transform.localRotation;
        }
    }

    Vector3 GetTargetPosition(Vector3 currentPosition, Vector3 anchorPosition)
    {
        return anchorPosition - currentPosition;
    }

    Quaternion GetTargetRotation(Quaternion currentRotation, Quaternion anchorRotation)
    {
        return Quaternion.Inverse(currentRotation) * anchorRotation;
    }
}
