using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using Steamworks;

public class PlayerBehavior : NetworkBehaviour
{
   public GameObject leftHand;
   public GameObject rightHand;
   public GameObject leftFoot;
   public GameObject rightFoot;

   public Material otherPlayer;

   public int playerCount = 0;
   
   private List<PlayerController> playerList;

   public Text username;

   private GameObject weatherManager;

   public Light flashLight;

   [Client]
   void Start()
   {
      weatherManager = GameObject.FindGameObjectWithTag("Weather");
      if (!isLocalPlayer)
      {
         GetComponentInChildren<Rigidbody>().isKinematic = true;
         GetComponentInChildren<MouseBehavior>().enabled = false;
         GetComponentInChildren<RightFootBehavior>().enabled = false;
         GetComponentInChildren<RightHandBehavior>().enabled = false;
         GetComponentInChildren<LeftFootBehavior>().enabled = false;
         GetComponentInChildren<LeftHandBehavior>().enabled = false;
         GetComponentInChildren<CameraBehavior>().enabled = false;
         GetComponentInChildren<RopeScript>().enabled = false;
      }
      if ((isLocalPlayer && isServer) || (!isLocalPlayer && !isServer))
      {
         GetComponentInChildren<Renderer>().material = otherPlayer;
      }
      if (weatherManager.GetComponent<WeatherManagerBehavior>().night || weatherManager.GetComponent<WeatherManagerBehavior>().dusk)
      {
         flashLight.enabled = true;
      }
      else
      {
         flashLight.enabled = false;
      }
   }

   [Client]
   public override void OnStartLocalPlayer()
   {
      GetComponentInChildren<Rigidbody>().isKinematic = false;
      GetComponentInChildren<MouseBehavior>().enabled = true;
      GetComponentInChildren<RightFootBehavior>().enabled = true;
      GetComponentInChildren<RightHandBehavior>().enabled = true;
      GetComponentInChildren<LeftFootBehavior>().enabled = true;
      GetComponentInChildren<LeftHandBehavior>().enabled = true;
      GetComponentInChildren<CameraBehavior>().enabled = true;
      GetComponentInChildren<RopeScript>().enabled = true;
   }

   public Transform getPlayerToAttachTo()
   {
      PlayerController player = null;
      playerList = connectionToServer.playerControllers;
      for (int i = 0; i < playerList.Count; i++)
      {
         Debug.Log(playerList[i].gameObject.name);
         if (playerList[i].playerControllerId != this.playerControllerId)
         {
            player = playerList[i];
            i = playerList.Count;
         }
      }
      if (player != null)
      {
         return player.gameObject.transform.Find("BearBones/Middle_Spine").transform;
      }
      return null;
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

   [Command]
   public void CmdCreateJoint(int type, Vector3 pos)
   {
      RpcCreateJoint(type, pos);
   }

   [ClientRpc]
   public void RpcCreateJoint(int type, Vector3 pos)
   {
      if (!isLocalPlayer)
      {
         if (type == 0 && rightHand.GetComponent<HingeJoint>() == null)
         {
            rightHand.transform.position = pos;
            rightHand.GetComponent<Light>().intensity = 10.0f;
            rightHand.gameObject.AddComponent<HingeJoint>();
            rightHand.GetComponent<HingeJoint>().enablePreprocessing = false;
            rightHand.GetComponent<HingeJoint>().useSpring = true;
         }
         else if (type == 1 && leftHand.GetComponent<HingeJoint>() == null)
         {
            leftHand.transform.position = pos;
            leftHand.GetComponent<Light>().intensity = 10.0f;
            leftHand.gameObject.AddComponent<HingeJoint>();
            leftHand.GetComponent<HingeJoint>().enablePreprocessing = false;
            leftHand.GetComponent<HingeJoint>().useSpring = true;
         }
         else if (type == 2 && rightFoot.GetComponent<HingeJoint>() == null)
         {
            rightFoot.transform.position = pos;
            rightFoot.GetComponent<Light>().intensity = 10.0f;
            rightFoot.gameObject.AddComponent<HingeJoint>();
            rightFoot.GetComponent<HingeJoint>().enablePreprocessing = false;
            rightFoot.GetComponent<HingeJoint>().useSpring = true;
         }
         else if (type == 3 && leftFoot.GetComponent<HingeJoint>() == null)
         {
            leftFoot.transform.position = pos;
            leftFoot.GetComponent<Light>().intensity = 10.0f;
            leftFoot.gameObject.AddComponent<HingeJoint>();
            leftFoot.GetComponent<HingeJoint>().enablePreprocessing = false;
            leftFoot.GetComponent<HingeJoint>().useSpring = true;
         }
      }
   }

   [Command]
   public void CmdDestroyJoint(int type)
   {
      RpcDestroyJoint(type);
   }

   [ClientRpc]
   public void RpcDestroyJoint(int type)
   {
      if (!isLocalPlayer)
      {
         if (type == 0)
         {
            rightHand.GetComponent<Light>().intensity = 0.0f;
            if (rightHand.GetComponent<HingeJoint>() != null)
            {
               Destroy(rightHand.GetComponent<HingeJoint>());
            }
         }
         else if (type == 1)
         {
            leftHand.GetComponent<Light>().intensity = 0.0f;
            if (leftHand.GetComponent<HingeJoint>() != null)
            {
               Destroy(leftHand.GetComponent<HingeJoint>());
            }
         }
         else if (type == 2)
         {
            rightFoot.GetComponent<Light>().intensity = 0.0f;
            if (rightFoot.GetComponent<HingeJoint>() != null)
            {
               Destroy(rightFoot.GetComponent<HingeJoint>());
            }
         }
         else if (type == 3)
         {
            leftFoot.GetComponent<Light>().intensity = 0.0f;
            if (leftFoot.GetComponent<HingeJoint>() != null)
            {
               Destroy(leftFoot.GetComponent<HingeJoint>());
            }
         }
      }
   }
}
