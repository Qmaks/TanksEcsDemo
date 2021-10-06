using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthUISystem))]
public sealed class HealthUISystem : LateUpdateSystem
{
    public GameObject ViewPrefab;
   
    private Transform Canvas;
    private Filter filterCreate;
    private Filter filterUpdate;

    
    public override void OnAwake()
    {
        filterCreate = World.Filter.With<HealthUIComponent>().With<CreatedComponent>();
        filterUpdate = World.Filter.With<HealthUIComponent>();
        Canvas = FindObjectOfType<SceneReference>().Canvas;
    }

    public override void OnUpdate(float deltaTime)
    {
        CreateNewHealthUI();
        UpdateHealthUI();
    }

    private void CreateNewHealthUI()
    {
        foreach (var entity in filterCreate)
        {
            entity.RemoveComponent<CreatedComponent>();
            var healthGo = Instantiate(ViewPrefab, Canvas);
            ref var healthUI = ref entity.GetComponent<HealthUIComponent>();
            healthUI.View = healthGo.GetComponent<HealthBarUI>();
        }
    }
    private void UpdateHealthUI()
    {
        foreach (var entity in filterUpdate)
        {
            ref var healthUI     = ref entity.GetComponent<HealthUIComponent>();
            ref var transform    = ref entity.GetComponent<TransformRef>();
            ref var targetHealth = ref entity.GetComponent<HealthComponent>();
            
            healthUI.View.SetHealthBar(targetHealth.HealthCurrent/targetHealth.HealthMax);
            healthUI.View.transform.position = Camera.main.WorldToScreenPoint(transform.Transform.position);

            /*
            ref var healthUI = ref entity.GetComponent<HealthUIComponent>();
            ref var targetHealth = ref healthUI.TargetEntity.GetComponent<HealthComponent>();
            healthUI.View.SetHealthBar(targetHealth.HealthCurrent/targetHealth.HealthMax);
            ref var targetTransform = ref healthUI.TargetEntity.GetComponent<TransformRef>();
            healthUI.View.transform.position = Camera.main.WorldToScreenPoint(targetTransform.Transform.position);
            */
        }
    }
}