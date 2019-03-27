using Unity.Entities;

public struct FixedOrbitComponent : IComponentData
{
    public float radius;
    public float period;
    public float epoch;
    public float x;
    public float y;
    public Entity parent;
}
