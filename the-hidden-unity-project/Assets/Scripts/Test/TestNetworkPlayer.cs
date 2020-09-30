using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TestNetworkPlayer : NetworkBehaviour
{
    private InputMaster _inputMaster;

    public GameObject currentModel;
    public GameObject networkPlayerModel;

    private bool _usingNetworkPlayerModel = false;

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
                _inputMaster.Enable();
                _inputMaster.FpsController.Movement.performed += context => OnMove(context.ReadValue<Vector2>());
            }
        }
        else
        {
            GetComponent<Camera>().enabled = false;
            
            if (!_usingNetworkPlayerModel)
            {
                Destroy(currentModel);
                networkPlayerModel.SetActive(true);
                _usingNetworkPlayerModel = true;
            }
        }

//        Debug.Log(isServer + " " + isClient + " " + isLocalPlayer);
    }

    private void OnMove(Vector2 move)
    {
        transform.Translate(move);
    }
}