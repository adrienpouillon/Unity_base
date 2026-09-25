using UnityEngine;

public class LiquidUp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] public float _speed;
    [SerializeField] public Vector3 _direction;
    [SerializeField] public float _increaseTime;
    [SerializeField] public float _increaseSpeed;

    [Header("Component required")]
    [SerializeField] public Clock _clockUp;
    [SerializeField] public GameObject _target;


    void Reset()
    {
        _speed = 0.1f;
        _direction = new Vector3(0, 1, 0);
        _clockUp = GetComponent<Clock>();
        _increaseTime = 0.01f;
        _increaseSpeed = 0f;
    }

    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _clockUp.OnFinish += Move;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        _target.transform.Translate(_direction * _speed);
        _clockUp.TimeClock += _increaseTime;
        _speed += _increaseSpeed;
    }


}
