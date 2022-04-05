using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parralax;

    private Transform cam;
    private Vector3 lastCam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        lastCam = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCam;
        transform.position += delta * parralax;
        lastCam = cam.position;
    }
}
