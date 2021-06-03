using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

public class GoToGallery : MonoBehaviour
{
    Vector2 filteredPoint;
    public float headGazeContribution;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;
        filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, (0.5f));
        Quaternion headPose = TobiiAPI.GetHeadPose().Rotation;

        Quaternion headPoseAngle = new Quaternion(headPose.x * headGazeContribution, headPose.y * headGazeContribution, 0, headPose.w);
        Quaternion gazePointAngle = new Quaternion((-filteredPoint.y + 0.5f) * (1 - headGazeContribution), (filteredPoint.x - 0.5f) * (1 - headGazeContribution), 0, headPose.w);
        SceneManager.LoadScene("GalleryScene");
    }
}
