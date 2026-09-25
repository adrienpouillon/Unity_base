using System;
using UnityEngine;
using NaughtyAttributes;

public class LifeComponent : MonoBehaviour
{
    [Header("Config PV")]
    [SerializeField] public int _startPv;
    [SerializeField] public int _maxPv;
    [SerializeField] public int _minPv;

    [Header("Config Action")]
    [SerializeField] public int _health;
    [SerializeField] public int _damage;
    [SerializeField] public bool _deadCanDestroy;

    public event Action<int> OnUpdateLife;
    public event Action<int> OnHealthAll;
    public event Action<int> OnHealthLittle;
    public event Action<int> OnDamageAll;
    public event Action<int> OnDamageLittle;
    public event Action<int> OnDead;


    [SerializeField, ReadOnly] int _pv;
    public int CurrentPV
    {
        get { return _pv; }
        set
        {
            if (IsDead())
            {
                Debug.LogError("PV ne peut pas être en dessous de 0");
            }
            _pv = value;
        }
    }

    void Reset()
    {
        _startPv = 100;
        _maxPv = 100;
        _minPv = 0;
        
        _damage = 1;
        _health = 1;
        _deadCanDestroy = true;

    }
    void Awake()
    {
        AllHealth();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_deadCanDestroy && IsDead())
        {
            Destroy(gameObject);
        }
    }

    public void AllHealth()
    {
        _pv = _startPv;
        OnUpdateLife?.Invoke(_pv);
        OnHealthAll?.Invoke(_pv);
    }

    public void DefaultHealth()
    {
        _pv += _health;
        if (_pv > _maxPv)
        {
            _pv = _maxPv;
        }
        OnUpdateLife?.Invoke(_pv);
        OnHealthLittle?.Invoke(_pv);
    }

    public void CustomHealth(int health)
    {
        _pv = health;
        if (_pv > _maxPv)
        {
            _pv = _maxPv;
        }
        OnUpdateLife?.Invoke(_pv);
        OnHealthLittle?.Invoke(_pv);
    }


    public void DefaultDamageAll()
    {
        _pv = _minPv;
        OnUpdateLife?.Invoke(_pv);
        OnDamageAll?.Invoke(_pv);
    }
    public void DefaultDamage()
    {
        _pv -= _damage;
        OnUpdateLife?.Invoke(_pv);
        OnDamageLittle?.Invoke(_pv);
    }

    public void CustomDamage(int damage)
    {
        _pv = damage;
        OnUpdateLife?.Invoke(_pv);
        OnDamageLittle?.Invoke(_pv);
    }

    public bool IsDead()
    {
        if (_pv <= _minPv)
        {
            return true;
        }
        return false;
    }

    /*
    int Method()
    {
        this.PV = 12;
        return this.PV;
    }
    */
}
