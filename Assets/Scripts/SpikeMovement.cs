using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private LayerMask _layersToCheck;

    private Vector3 _targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
        StartCoroutine(RaycastingForMovement());
    }
    IEnumerator RaycastingForMovement()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();

            if (transform.position != _targetPosition)
                continue;

            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), fwd, out hit, 0.4f, _layersToCheck))
            {
                Rotate();
            }
            else//ako nije
            {
                _targetPosition = transform.position + fwd * 0.4f;
                StopCoroutine("Move");
                StartCoroutine(Move());
            }

            Debug.DrawLine(transform.position + new Vector3(0, 0.2f, 0), transform.position + fwd * 0.4f + new Vector3(0, 0.2f, 0));
            
        }

    }
    IEnumerator Move()
    {
        Vector3 startingPos = transform.position;
        float time = 0;
        while (true)
        {
            time += _speed * Time.deltaTime;
            if (time > 1)
                time = 1;

            transform.position = Vector3.Lerp(startingPos, _targetPosition, time);
            if (transform.position == _targetPosition)
                break;

            yield return new WaitForEndOfFrame();
        }
    }

    private void Rotate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
    }

}
