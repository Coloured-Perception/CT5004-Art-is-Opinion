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
    public bool isTobii;
    public float startX;
    public float startY;

    public bool contiuousRotate;

    Quaternion camRotation;

    public Camera cam;
    Vector2 filteredPoint;

    // Update is called once per frame
    void Update()
    {
        if (isTobii)
        {
            UserPresence isPresent = TobiiAPI.GetUserPresence();
            if (isPresent == UserPresence.Present)
            {
                Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;
                filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, (1 - responsiveness));
                Quaternion headPose = TobiiAPI.GetHeadPose().Rotation;

                Quaternion headPoseAngle = new Quaternion(headPose.x * headGazeContribution, headPose.y * headGazeContribution, 0, headPose.w);
                Quaternion gazePointAngle = new Quaternion((-filteredPoint.y + 0.5f) * (1 - headGazeContribution), (filteredPoint.x - 0.5f) * (1 - headGazeContribution), 0, headPose.w);

                if (((headPoseAngle.x + gazePointAngle.x) * sensitivity) > maxAngle)
                {
                    if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngle)
                    {
                        camRotation = new Quaternion(maxAngle, maxAngle, 0, headPose.w);
                    }
                    else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < -maxAngle)
                    {
                        camRotation = new Quaternion(maxAngle, -maxAngle, 0, headPose.w);
                    }
                    else
                    {
                        camRotation = new Quaternion(maxAngle, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);
                    }
                }
                else if (((headPoseAngle.x + gazePointAngle.x) * sensitivity) < -maxAngle)
                {
                    if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngle)
                    {
                        camRotation = new Quaternion(-maxAngle, maxAngle, 0, headPose.w);
                    }
                    else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < -maxAngle)
                    {
                        camRotation = new Quaternion(-maxAngle, -maxAngle, 0, headPose.w);
                    }
                    else
                    {
                        camRotation = new Quaternion(-maxAngle, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);
                    }
                }
                else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngle)
                {
                    camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, maxAngle, 0, headPose.w);
                }
                else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < -maxAngle)
                {
                    camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, -maxAngle, 0, headPose.w);
                }
                else
                {
                    camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);

                }

                if (contiuousRotate)
                {
                    cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x + camRotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + camRotation.eulerAngles.y, 0);
                    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, cam.transform.rotation * camRotation, Time.deltaTime * speed);
                }
                else
                {
                    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camRotation, Time.deltaTime * speed);
                }
            }
            else
            {
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, new Quaternion(0, 0, 0, 1), Time.deltaTime * speed);
            }
        }
        else
        {
            float xSpeed = 0;
            float ySpeed = 0;


            if (Input.GetKey("up"))
            {
                xSpeed -= 1;
            }
            if (Input.GetKey("down"))
            {
                xSpeed += 1;
            }
            if (Input.GetKey("left"))
            {
                ySpeed -= 1;
            }
            if (Input.GetKey("right"))
            {
                ySpeed += 1;
            }
            camRotation = new Quaternion(xSpeed * sensitivity, ySpeed * sensitivity, 0, 0);
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x + xSpeed, cam.transform.rotation.eulerAngles.y + ySpeed, 0);
        }
    }
}