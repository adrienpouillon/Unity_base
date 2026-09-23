using UnityEngine;
using UnityEngine.UI;

public class LifeBarre : MonoBehaviour
{
    [Header("Component required")]
    [SerializeField] Slider _slider;
    [SerializeField] LifeComponent _life;

    void Reset()
    {
        _life = GetComponent<LifeComponent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _life.OnUpdateLife += SliderUpdate;

        _slider.maxValue = _life._maxPv;
        _slider.value = _life.CurrentPV;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnDestroy()
    {
        _life.OnUpdateLife -= SliderUpdate;
    }

    void SliderUpdate(int currentLife)
    {
        _slider.value = currentLife;
    }

}
