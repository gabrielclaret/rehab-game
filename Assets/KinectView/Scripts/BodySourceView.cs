using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject handLeft;
    public GameObject handRight;
    public GameObject elbowLeft;
    public GameObject elbowRight;
    public GameObject shoulderLeft;
    public GameObject shoulderRight;

	[SerializeField] private bool mostrarEsqueletoJogador = false;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Update()
    {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            if (
                !( jt == Kinect.JointType.FootLeft
                || jt == Kinect.JointType.FootRight
                || jt == Kinect.JointType.ThumbLeft
                || jt == Kinect.JointType.ThumbRight
                || jt == Kinect.JointType.HandTipRight
                || jt == Kinect.JointType.HandTipLeft
                || jt == Kinect.JointType.Neck
                || jt == Kinect.JointType.Head)
             )   
            {
                GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

                LineRenderer lr = jointObj.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.01f, 0.01f);

                jointObj.transform.localScale = new Vector3(0, 0, 0);
                jointObj.name = jt.ToString();
                jointObj.transform.parent = body.transform;
            }
        }
        return body;
    }

   

    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            /* 
             *  if ( jt == Kinect.JointType.HandRight 
                 || jt == Kinect.JointType.HandLeft  
                 || jt == Kinect.JointType.ShoulderRight 
                 || jt == Kinect.JointType.ShoulderLeft  
                 || jt == Kinect.JointType.ElbowRight 
                 || jt == Kinect.JointType.ElbowLeft
                 || jt == Kinect.JointType.WristRight
                 || jt == Kinect.JointType.WristLeft
                 || jt == Kinect.JointType.SpineBase
                 || jt == Kinect.JointType.SpineMid
             )
             * if (
                !(jt == Kinect.JointType.HipLeft
                || jt == Kinect.JointType.HipRight
                || jt == Kinect.JointType.ThumbLeft
                || jt == Kinect.JointType.ThumbRight
                || jt == Kinect.JointType.HandTipRight
                || jt == Kinect.JointType.HandTipLeft
                || jt == Kinect.JointType.Neck
                || jt == Kinect.JointType.Head
                || jt == Kinect.JointType.SpineBase)
             )*/

            if (!( jt == Kinect.JointType.FootLeft
                || jt == Kinect.JointType.FootRight
                || jt == Kinect.JointType.ThumbLeft
                || jt == Kinect.JointType.ThumbRight
                || jt == Kinect.JointType.HandTipRight
                || jt == Kinect.JointType.HandTipLeft
                || jt == Kinect.JointType.Neck
                || jt == Kinect.JointType.Head)
               )
            { 
                Kinect.Joint sourceJoint = body.Joints[jt];
                Kinect.Joint? targetJoint = null;
                if (jt == Kinect.JointType.HandLeft)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    handLeft.transform.position = pos;
                    //Debug.Log (pos.z);
                }
                if (jt == Kinect.JointType.HandRight)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    handRight.transform.position = pos;
                }
                if (jt == Kinect.JointType.ElbowLeft)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    elbowLeft.transform.position = pos;
                }
                if (jt == Kinect.JointType.ElbowRight)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    elbowRight.transform.position = pos;
                }
                if (jt == Kinect.JointType.ShoulderLeft)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    shoulderLeft.transform.position = pos;
                }
                if (jt == Kinect.JointType.ShoulderRight)
                {
                    Vector3 pos = GetVector3FromJoint(sourceJoint);
                    shoulderRight.transform.position = pos;
                }
                if (_BoneMap.ContainsKey(jt))
                {
                    targetJoint = body.Joints[_BoneMap[jt]];
                }

                Transform jointObj = bodyObject.transform.Find(jt.ToString());
                jointObj.localPosition = GetVector3FromJoint(sourceJoint);
				if (mostrarEsqueletoJogador) {
					LineRenderer lr = jointObj.GetComponent<LineRenderer> ();
					if (targetJoint.HasValue) {
						lr.SetPosition (0, jointObj.localPosition);
						lr.SetPosition (1, GetVector3FromJoint (targetJoint.Value));
						lr.SetColors (GetColorForState (sourceJoint.TrackingState), GetColorForState (targetJoint.Value.TrackingState));
					} else {
						lr.enabled = false;
					}
				}
            }
        }
    }

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 5, joint.Position.Y * 5, 10 + joint.Position.Z);
    }

}
