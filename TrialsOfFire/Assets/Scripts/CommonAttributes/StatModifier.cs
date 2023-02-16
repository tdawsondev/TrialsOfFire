using System;
[Serializable]
public class StatModifier
{
    public float Value;
    public int Order;
    public object Source;

    public StatModifier(float value, int order, object source)
    {
        Value = value;
        Order = order;
        Source = source;
    }
    public StatModifier(float value, int order) : this(value, order, null) { }
    public StatModifier(float value): this(value, 0) { }
}
