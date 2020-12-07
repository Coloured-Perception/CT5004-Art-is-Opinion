using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    private int m_difficulty;

    public int Difficulty
    {
        get { return m_difficulty; }
        set { m_difficulty = value; }
    }
}
