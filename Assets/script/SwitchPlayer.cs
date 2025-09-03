using UnityEngine;
using Fungus;
using Cinemachine;
using System.Collections.Generic;

[CommandInfo("Custom", "Switch Player", "切換GameManager.PlayerName控制權和攝影機目標，並同步同名子物件的啟用狀態")]
public class SwitchPlayer : Command
{
    public GameObject playerA;
    public GameObject playerB;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public bool switchToB = true;

    private Dictionary<string, bool> activeChildStates = new Dictionary<string, bool>();

    private void SwitchToPlayer(GameObject fromPlayer, GameObject toPlayer)
    {
        RecordActiveChildStates(fromPlayer);

        fromPlayer.SetActive(false);
        toPlayer.SetActive(true);
        virtualCamera.Follow = toPlayer.transform;

        ApplyChildStatesByName(toPlayer);
    }

    public override void OnEnter()
    {
        if (switchToB)
        {
            SwitchToPlayer(playerA, playerB);
        }
        else
        {
            // 繼續用A，不用改子物件狀態
            playerA.SetActive(true);
            playerB.SetActive(false);
            virtualCamera.Follow = playerA.transform;
        }

        Continue();
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
