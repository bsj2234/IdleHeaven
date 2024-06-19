//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestingInrefaceisNullWhengameObjectisDestroyed : MonoBehaviour
//{
//    public AICharacterController characterController;
//    ICombat interfaceTest;

//    private void Start()
//    {
//        interfaceTest = characterController;
//        Destroy(characterController.gameObject);
//        if(interfaceTest == null)
//        {
//            Debug.Log("== works fine");
//        }
//        else if (interfaceTest.GetGameObject() == null)
//        {
//            Debug.Log("== not works need custom beheiv");
//        }
//        else
//        {
//            Debug.Log("i got no idea");
//        }

//    }

//    private void Update()
//    {

//        if (interfaceTest == null)
//        {
//            Debug.Log("== works fine");
//        }
//        else if (interfaceTest.GetGameObject() == null)
//        {
//            Debug.Log("== not works need custom beheiv");
//        }
//        else
//        {
//            Debug.Log("i got no idea");
//        }
//    }
//}
