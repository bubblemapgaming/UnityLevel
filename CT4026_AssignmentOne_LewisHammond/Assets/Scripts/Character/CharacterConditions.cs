using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConditions : MonoBehaviour {

    #region Vars

    private bool escapeInitiated = false;
    private float health = 100;

    [SerializeField]
    private int bombs = 20;

    public bool isEscaping
    {
        get { return escapeInitiated; }
        set { escapeInitiated = value; }
    }

    public float playerHealth
    {
        get { return health; }
        set { health = value; }
    }

    public int bombCount
    {
        get { return bombs; }
        set { bombs = value; }
    }

    #endregion

    #region Events


    #endregion


    //Get and Set for initating player escaping status
    public void InitiatePlayerEscape()
    {
        escapeInitiated = true;
    }

    private void PlayerTakeDamage()
    {

    }


}
