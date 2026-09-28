using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class Routine : MonoBehaviour
{
    [SerializeField] float _delay;

    CancellationTokenSource _cts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        Spawn(_cts.Token);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Debug.Log("Spawning");
            yield return new WaitForSeconds(_delay);
        }
    }

    async void Spawn(CancellationTokenSource cts)
    {
        while (true)
        {
            Debug.Log("Spawning");

            if(cts.IsCancellationResquested)
            {

            }

            await Awaitable.WaitForSecondsAsync(10.f, destroyCancellationToken);
            await Awaitable.NextFrameAsync();
        }
    }

    void OnDestroy()
    {

    }
}
