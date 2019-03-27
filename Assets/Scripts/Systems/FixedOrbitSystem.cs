using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class FixedOrbitSystem : JobComponentSystem
{
    private ComponentGroup m_Group;

    protected override void OnCreateManager()
    {
        m_Group = GetComponentGroup(ComponentType.ReadOnly<FixedOrbitComponent>());
    }

    [BurstCompile]
    struct FixedOrbitJob : IJobChunk
    {
        public float deltaTime;
        void IJobChunk.Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
           
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}
