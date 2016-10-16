using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyManagerBehavior : NetworkLobbyManager
{
   void Update()
   {
      minPlayers = numPlayers;
   }
}
