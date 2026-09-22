using UnityEngine;
using System;

public class Super_component : MonoBehaviour
{
    [SerializeField] Vector3 _dir;
    [SerializeField] bool _useForward;
    [SerializeField] float _speed;
    [SerializeField] InputActionReference _moveInput;

    int _pv;

    Rigidbody _rb;

    public int PV
    {
        get { return _pv; }
        set 
        {
            if (_pv < 0)
            {
                Debug.LogError("PV ne peut pas être en dessous de 0");
            }
            _pv = value;
        }
    }

    void Reset()
    {
        _dir = new Vector3(0,0,1);
        _useForward = true;
        _speed = 1;
    }

    void Awake()
    {
        _pv = 100;
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _moveInput.action.ReadValue<Vector2>();
        if (_useForward)
        {
            TranslateAll(direction);
        }
        else
        {
            TranslateForward(direction);
        }
    }

    /*void FixedUpdate()
    {
        _rb.AddForce(10, 0, 0);
    }*/

    void TranslateAll(Vector2 direction)
    {
        transform.Translate(_dir * direction * Time.deltaTime);
    }

    void TranslateForward(Vector2 direction)
    {
        transform.Translate(transform.forward * direction * (Time.deltaTime * _speed));
    }

    int Method()
    {
        this.PV = 12;
        //Instantiate();
        return this.PV;
    }

    public event Action OnDamage;
}
