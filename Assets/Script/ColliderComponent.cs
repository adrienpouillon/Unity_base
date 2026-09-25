using UnityEngine;
using UnityEngine.Events;
public struct Lock
{
    bool _beginLock;
    bool _endLock;

    public Lock(bool beginLock, bool endLock) : this()
    {
        _beginLock = beginLock;
        _endLock = endLock;
    }

    public static Lock operator ++(Lock l)
    {
        if (l._beginLock)
        {
            if (l._endLock)
            {
                return new Lock(false, false);
            }
            return new Lock(true, true);
        }
        return new Lock(true, false);
    }
}


public class ColliderComponent : MonoBehaviour
{
    [Header("Component required")]
    [SerializeField] LifeComponent _life;
    //[SerializeField] MoveComponent _move;
    
    [Header("Event Feedback")]
    [SerializeField] UnityEvent OnDamageTouchTag;
    [SerializeField] UnityEvent OnHealthTouchTag;

    //Lock _lockMove;

    void Reset()
    {
        _life = GetComponent<LifeComponent>();
        //_move = GetComponent<MoveComponent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        /*if (other.gameObject.CompareTag("Sol"))
        {
            _pv--;
        }*/
        /*GroundTag gt = other.gameObject.GetComponent<GroundTag>();
        if(gt != null)
        {
            _pv--;
        }*/
        if (other.gameObject.TryGetComponent(out HealthTouchTag healthTouchTag))
        {
            _life.DefaultHealth();
            OnHealthTouchTag.Invoke();
        }

        if (other.gameObject.TryGetComponent(out DamageTouchTag damageTouchTag))
        {
            _life.DefaultDamage();
            OnDamageTouchTag.Invoke();
        }
    }
}
