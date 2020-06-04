using System;

[Serializable]
public class IntRange
{
    public int m_Min, m_Max;

    public IntRange(int min,int max)
    {
        m_Max = max;
        m_Min = min;
    }

    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}

