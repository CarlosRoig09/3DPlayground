using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaivour : MonoBehaviour, ICanBeImpulse
{
    private Rigidbody _rb;
    public void GetImpulse(Vector3 impulse)
    {
        _rb.AddForce(impulse);
    }

    public void StopMomentum()
    {
        _rb.velocity= Vector3.zero;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
      /*  if(TryGetComponent<ICanBeImpulse>(out var canBeImpulse))
        {
            if(canBeImpulse!=null)
            {
                canBeImpulse.GetImpulse(TrasspassImpulse(collision.transform.position - transform.position));
            }
        }*/
    }

    public Vector3 TrasspassImpulse(Vector3 impulse)
    {
        return new Vector3(impulse.x * _rb.velocity.x, impulse.y * _rb.velocity.y, impulse.z * _rb.velocity.z);
    }

    public void OnHit()
    {
        throw new System.NotImplementedException();
    }
}
