using SoftGear.Strix.Unity.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : StrixBehaviour
{
    #region  �v���C�x�[�g

    #endregion

    #region  �p�u���b�N
    // �v���C���[1
    [StrixSyncField] public int p1_attack = 10;  // �v���C���[1�̍U����
    [StrixSyncField] public int p1_hp = 100;     // �v���C���[1��HP
    [StrixSyncField] public int g_p1_hp;

    // �v���C���[2
    [StrixSyncField] public int p2_attack = 10;  // �v���C���[2�̍U����
    [StrixSyncField] public int p2_hp = 100;     // �v���C���[2��HP
    [StrixSyncField] public int g_p2_hp;
    #endregion

    private void Start()
    {
        g_p1_hp = p1_hp;
        g_p2_hp = p2_hp;
    }
}
