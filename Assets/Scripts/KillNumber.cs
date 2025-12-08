using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KillNumber : MonoBehaviour
{
    [SerializeField]
    private int m_killNumbers;
    public int KillNumbers { get { return m_killNumbers; } }

    public void AddNumber() { m_killNumbers++; }
}
