using System.Collections;
using System.Collections.Generic;
using Morpeh;
using Morpeh.Globals;
using UnityEngine;

public class PlayerRestartCourutin : MonoBehaviour
{
    public GlobalEvent OnPlayerDestroy;
    
    void Start()
    {
        OnPlayerDestroy.Subscribe(ints =>
        {
            StartCoroutine(RestartPlayer());
        });
    }

    private IEnumerator RestartPlayer()
    {
        yield return new WaitForSeconds(2f);
        var entity = World.Default.CreateEntity();
        entity.AddComponent<CreatePlayerCommand>();
    }
}
