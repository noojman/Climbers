using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class LobbyPlayerBehavior : NetworkBehaviour
{
   public GameObject leftHand;
   public GameObject rightHand;
   public GameObject leftFoot;
   public GameObject rightFoot;
   
   private Button[] buttons;

   public Material otherPlayer;

   public Text username;

   private Scene startScene;

   [Client]
   void Start()
   {
      startScene = SceneManager.GetActiveScene();
      buttons = FindObjectsOfType<Button>();
      foreach (Button button in buttons)
      {
         if (button.name == "ReadyButton" && isLocalPlayer)
         {
            button.onClick.AddListener(delegate ()
            {
               GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
            });
         }
      }

      if (!isLocalPlayer)
      {
         GetComponentInChildren<Rigidbody>().isKinematic = true;
         GetComponentInChildren<MouseBehavior>().enabled = false;
      }

      if ((isLocalPlayer && isServer) || (!isLocalPlayer && !isServer))
      {
         GetComponentInChildren<Renderer>().material = otherPlayer;
      }
   }

   void Update()
   {
      if (SceneManager.GetActiveScene() != startScene)
      {
         Destroy(this);
         //TODO - maybe make it kinematic, and then switch back... depends on if going back to the lobby recreates lobbyplayer
      }
   }

   [Client]
   public override void OnStartLocalPlayer()
   {
      transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
      GetComponentInChildren<Rigidbody>().isKinematic = false;
      GetComponentInChildren<MouseBehavior>().enabled = true;
   }


   [Command]
   public void CmdActivateCanvas() // TODO - call this in OnLobbyClientEnter when creating custom network manager
   { // possibly not the right solution, but figure out a way so that every player sees the label for other players
      RpcActivateCanvas();
   }

   [ClientRpc]
   public void RpcActivateCanvas()
   {
      if (SteamManager.Initialized && !isLocalPlayer)
      {
         username.text = SteamFriends.GetPersonaName();
      }
   }
}
