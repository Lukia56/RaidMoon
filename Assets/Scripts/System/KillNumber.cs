using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KillNumber : MonoBehaviour
{
    static public int s_killNumbers;
    public int KillNumbers { get { return s_killNumbers; } }

    public void AddNumber() { s_killNumbers++; }
}
