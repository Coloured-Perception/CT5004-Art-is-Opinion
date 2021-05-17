using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class RotateCamera : MonoBehaviour
{
    public float headGazeContribution;
    public float maxAngleX;
    public float maxAngleY;
    public float minAngleX;
    public float minAngleY;
    public float sensitivity;
    public float responsiveness;
    public float speed;
    public GameObject isEyeTracker;

    public bool continuousRotate;

    Quaternion camRotation;

    float angleX;
    float angleY;
    float xSpeed;
    float ySpeed;

    public Camera cam;
    Vector2 filteredPoint;

    // Update is called once per frame
    void Update()
    {
        if(cam.GetComponent<Animator>().applyRootMotion == true)
        {
            if (isEyeTracker.GetComponent<isEyeTrackerUsed>().isEyeTracker)
            {
                UserPresence isPresent = TobiiAPI.GetUserPresence();
                if (isPresent == UserPresence.Present)
                {
                    Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;
                    filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, (1 - responsiveness));
                    Quaternion headPose = TobiiAPI.GetHeadPose().Rotation;

                    Quaternion headPoseAngle = new Quaternion(headPose.x * headGazeContribution, headPose.y * headGazeContribution, 0, headPose.w);
                    Quaternion gazePointAngle = new Quaternion((-filteredPoint.y + 0.5f) * (1 - headGazeContribution), (filteredPoint.x - 0.5f) * (1 - headGazeContribution), 0, headPose.w);

                    if (continuousRotate)
                    {                        
                        xSpeed = (headPoseAngle.x + gazePointAngle.x) * sensitivity;
                        ySpeed = (headPoseAngle.y + gazePointAngle.y) * sensitivity;

                        ContinuousRotate(xSpeed,ySpeed);
                    }
                    else
                    {
                        if (((headPoseAngle.x + gazePointAngle.x) * sensitivity) > maxAngleX)
                        {
                            if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngleX)
                            {
                                camRotation = new Quaternion(maxAngleX, maxAngleY, 0, headPose.w);
                            }
                            else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < minAngleY)
                            {
                                camRotation = new Quaternion(maxAngleX, minAngleY, 0, headPose.w);
                            }
                            else
                            {
                                camRotation = new Quaternion(maxAngleX, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);
                            }
                        }
                        else if (((headPoseAngle.x + gazePointAngle.x) * sensitivity) < minAngleX)
                        {
                            if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngleX)
                            {
                                camRotation = new Quaternion(minAngleX, maxAngleY, 0, headPose.w);
                            }
                            else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < minAngleY)
                            {
                                camRotation = new Quaternion(minAngleX, minAngleY, 0, headPose.w);
                            }
                            else
                            {
                                camRotation = new Quaternion(minAngleX, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);
                            }
                        }
                        else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) > maxAngleY)
                        {
                            camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, maxAngleY, 0, headPose.w);
                        }
                        else if (((headPoseAngle.y + gazePointAngle.y) * sensitivity) < minAngleY)
                        {
                            camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, minAngleY, 0, headPose.w);
                        }
                        else
                        {
                            camRotation = new Quaternion((headPoseAngle.x + gazePointAngle.x) * sensitivity, (headPoseAngle.y + gazePointAngle.y) * sensitivity, 0, headPose.w);

                        }

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

                ContinuousRotate(xSpeed, ySpeed);
            }
        }
    }

    void ContinuousRotate( float xSpeed, float ySpeed)
    {

        float angleX = cam.transform.rotation.eulerAngles.x;
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        angleX = (angleX < -180) ? angleX + 360 : angleX;

        float angleY = cam.transform.rotation.eulerAngles.y;
        angleY = (angleY > 180) ? angleY - 360 : angleY;
        angleY = (angleY < -180) ? angleY + 360 : angleY;

        if (angleX + xSpeed > maxAngleX)
        {
            if (angleY + ySpeed > maxAngleY)
            {
                cam.transform.rotation = Quaternion.Euler(maxAngleX, maxAngleY, 0);
            }
            else if (angleY + ySpeed < minAngleY)
            {
                cam.transform.rotation = Quaternion.Euler(maxAngleX, minAngleY, 0);
            }
            else
            {
                cam.transform.rotation = Quaternion.Euler(maxAngleX, cam.transform.rotation.eulerAngles.y + ySpeed, 0);
            }
        }
        else if (angleX + xSpeed < minAngleX)
        {
            if (angleY + ySpeed > maxAngleY)
            {
                cam.transform.rotation = Quaternion.Euler(minAngleX, maxAngleY, 0);
            }
            else if (angleY + ySpeed < minAngleY)
            {
                cam.transform.rotation = Quaternion.Euler(minAngleX, minAngleY, 0);
            }
            else
            {
                cam.transform.rotation = Quaternion.Euler(minAngleX, cam.transform.rotation.eulerAngles.y + ySpeed, 0);
            }
        }
        else if (angleY + ySpeed > maxAngleY)
        {
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x + xSpeed, maxAngleY, 0);
        }
        else if (angleY + ySpeed < minAngleY)
        {
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x + xSpeed, minAngleY, 0);
        }
        else
        {
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x + xSpeed, cam.transform.rotation.eulerAngles.y + ySpeed, 0);
        }
    }
}