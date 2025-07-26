using UnityEngine;
using Fungus;
using Cinemachine;
using System.Collections.Generic;

[CommandInfo("Custom", "Switch Player", "切換主角控制權和攝影機目標，並同步同名子物件的啟用狀態")]
public class SwitchPlayer : Command
{
    public GameObject playerA;
    public GameObject playerB;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public bool switchToB = true;

    private Dictionary<string, bool> activeChildStates = new Dictionary<string, bool>();

    public override void OnEnter()
    {
        // 1. 先記錄目前主角啟用的子物件狀態（用名字來記）
        GameObject currentPlayer = switchToB ? playerA : playerB;
        RecordActiveChildStates(currentPlayer);

        // 2. 切換主角控制權與攝影機
        playerA.SetActive(!switchToB);
        playerB.SetActive(switchToB);
        virtualCamera.Follow = switchToB ? playerB.transform : playerA.transform;

        // 3. 將相同名稱的子物件狀態套用到新的主角
        GameObject newPlayer = switchToB ? playerB : playerA;
        ApplyChildStatesByName(newPlayer);

        Continue(); // Fungus 繼續執行
    }

    private void RecordActiveChildStates(GameObject player)
    {
        activeChildStates.Clear();
        foreach (Transform child in player.transform)
        {
            activeChildStates[child.name] = child.gameObject.activeSelf;
        }
    }

    private void ApplyChildStatesByName(GameObject player)
    {
        foreach (Transform child in player.transform)
        {
            if (activeChildStates.ContainsKey(child.name))
            {
                child.gameObject.SetActive(activeChildStates[child.name]);
            }
        }
    }
}
