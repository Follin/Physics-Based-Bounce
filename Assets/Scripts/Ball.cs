using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;

    private Vector3 _velocity;

    private float _friction = 0.0f;
    private float _radius;

    private void Awake()
    {
        _radius = GetComponent<SphereCollider>().radius;
    }

    void FixedUpdate()
    {
        if(!Physics.Raycast(transform.position, Vector3.down, _radius + 0.001f))
            _velocity += Physics.gravity * Time.fixedDeltaTime;

        _velocity -= _velocity * _friction * Time.fixedDeltaTime;

        RaycastHit hit;   
        if (Physics.SphereCast(transform.position, _radius, _velocity.normalized, out hit, _velocity.magnitude * Time.fixedDeltaTime, _hitMask))        
            _velocity = Vector3.Reflect(_velocity, hit.normal) * CheckPlane(hit.transform.gameObject.layer);        

        transform.position += _velocity * Time.fixedDeltaTime; 
    }

    float CheckPlane(int color)
    {
        switch(color)
        {
            case 8:
                //Debug.Log("Blue");
                return 0.9f;
            case 9:
                //Debug.Log("Orange");
                return 0.5f;
            case 10:
                //Debug.Log("Pink");
                return 1;

            default:
                //Debug.Log("Default");
                return 1;
        }
    }
}