using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float _time;
    [SerializeField] bool _autoReset;

    public event Action OnFinish;

    float _time_process = 0f;

    public float TimeClock
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
        }
    }

    void Reset()
    {
        _time = 1;
        _autoReset = true;
    }

    void Awake()
    {
        _time_process = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time_process += Time.deltaTime;
        if (IsFinish())
        {
            OnFinish.Invoke();
            if (_autoReset)
            {
                _time_process = 0f;
            }
        }
    }

    public bool IsFinish()
    {
        return _time_process > _time;
    }

    public void ResetClock()
    {
        _time_process = 0;
    }

}
