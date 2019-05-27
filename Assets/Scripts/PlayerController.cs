using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player[] players;

    Player          currentPlayer;
    int             playerIndex;
    CameraControl   ctrl;

    void Start()
    {
        playerIndex = 0;

        players = FindObjectsOfType<Player>();

        CameraControl ctrl = FindObjectOfType<CameraControl>();
        ctrl.target = players[playerIndex].transform;


        foreach (Player player in players)
        {
            player.OnControlledChanged += OnControlChanged;
        }
    }

    private void OnControlChanged(Player player)
    {
        if (!player.canControl)
            return;

        player.transform.position = players[playerIndex].transform.position;

        playerIndex++;

        if (playerIndex >= players.Length) playerIndex = 0;

        currentPlayer = player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentPlayer is Emma && !currentPlayer.isOnGround) return;

            foreach (Player player in players) player.CanControl = !player.canControl;

            CameraControl ctrl = FindObjectOfType<CameraControl>();
            if (ctrl)
            {
                ctrl.target = players[playerIndex].transform;
            }
        }
    }
}
