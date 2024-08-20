using SoftGear.Strix.Unity.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : StrixBehaviour
{
    #region  プライベート

    #endregion

    #region  パブリック
    // プレイヤー1
    [StrixSyncField] public int p1_attack = 10;  // プレイヤー1の攻撃力
    [StrixSyncField] public int p1_hp = 100;     // プレイヤー1のHP
    [StrixSyncField] public int g_p1_hp;

    // プレイヤー2
    [StrixSyncField] public int p2_attack = 10;  // プレイヤー2の攻撃力
    [StrixSyncField] public int p2_hp = 100;     // プレイヤー2のHP
    [StrixSyncField] public int g_p2_hp;
    #endregion

    private void Start()
    {
        g_p1_hp = p1_hp;
        g_p2_hp = p2_hp;
    }
}
