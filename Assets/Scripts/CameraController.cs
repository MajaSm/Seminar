using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//commit testaaaa
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private Camera _camera; 

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.position = _target.position + _offset;
        _camera.transform.LookAt(_target);
    }
}
