using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class RotateCamera : MonoBehaviour
{
    public float headGazeContribution;
    public float maxAngle;
    public float sensitivity;
    public float responsiveness;
    public float speed;

    public Camera cam;
    Vector2 filteredPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UserPresence isPresent= TobiiAPI.GetUserPresence();
        if( isPresent == UserPresence.Present)
        {
            Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;
            filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, (1 - responsiveness));
            Quaternion headPose = TobiiAPI.GetHeadPose().Rotation;

            Quaternion headPoseAngle = new Quaternion(headPose.x * headGazeContribution, headPose.y * headGazeContribution, 0, headPose.w); 
            Quaternion gazePointAngle = new Quaternion((-filteredPoint.y + 0.5f) * (1 - headGazeContribution), (filteredPoint.x - 0.5f) * (1 - headGazeContribution), 0, headPose.w);

            Quaternion camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);

            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camRotation, Time.time * speed);
        }
        else
        {
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, new Quaternion(0,0,0,1), Time.time * speed * 0.1f);
        }
    }
}
