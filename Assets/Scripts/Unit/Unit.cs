using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool IsFollowingCursor { get; set; }

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (IsFollowingCursor)
        {
            FollowCursor();
            return;
        }
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += mainCam.nearClipPlane;
        this.transform.position = mousePosition;
    }
}
