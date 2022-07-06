using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class feel : MonoBehaviour
// {
//     public static GameObject[] Shape_k;
//     static Vector3 CurrentPositionHolder;
//     static Vector3 startPosition;
//     float Timer;
//     int CurrentNode;
//     int MoveSpeed = 3;

//     // Start is called before the first frame update
//     void Start()
//     {
//         Shape_k = FindGameObjectsWithTag("l_shape");
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(newblockintroduced == true){
//             feel_outline();
//         }
//     }

//     void CheckNode(){
//         Timer = 0; 
//         CurrentPositionHolder = array[CurrentNode].transform.position;
//         startPosition = Player.transform.position;
//     }


//     void feel_outline(){
//         Timer += Time.deltaTime * MoveSpeed;
//         if(Player.transform.position != CurrentPositionHolder){
//             Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
//         }
//         else{
//             if(CurrentNode < Shape_k.Length - 1){
//                 CurrentNode++;
//                 CheckNode();
//             }
//         }
//     }
// }
