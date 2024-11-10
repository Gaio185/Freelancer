using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DivisionType
{
    DivisionA,
    DivisionB,
    DivisionC,
    DivisionD,
    DivisionE
}

public class Keycard : MonoBehaviour
{
    public DivisionType divisionType; // Set this in the Unity Editor to DivisionA, DivisionB, or DivisionC

    public DivisionType GetDivisionType()
    {
        return divisionType;
    }
}
