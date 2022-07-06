using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Spawn : MonoBehaviour
{
    public GameObject[] Pieces;
    private SpeechOut speechOut;
    public GameObject itPosition;
    public GameObject it;

    // Start is called before the first frame update
    async void Start()
    {
        LowerHandle lowerHandle;
        NewPiece(0);
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(itPosition);
        speechOut = new SpeechOut();
        await speechOut.Speak("For every new Level you will feel the blocks like this");
    }

    // Update is called once per frame
    public void NewPiece(int i)
    {
        it = Instantiate(Pieces[i], transform.position, Quaternion.identity);
    }

}