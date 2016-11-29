using UnityEngine;
using System.Collections;

public class Cameracontroller: MonoBehaviour {
    private Camera _mainCamera;

    public float UIEvalation;
    //public LayerMask TouchInputMask;

    /*public Vector3 MenuButtonPosition;
    public Vector3 HealthBarPosition;
    public Vector3 ScoreBoardPosition;


    public GameObject ScoreBoardPrefab;
    public GameObject MenuButtonPrefab;
    public GameObject HealthBarPrefab;

    public GameObject ScoreBoardDebug;
    public GameObject MenuButtonDebug;
    public GameObject HealthBarDebug;*/



    void Start() {
        //ScoreBoardDebug.SetActive(false);
        //MenuButtonDebug.SetActive(false);
        //HealthBarDebug.SetActive(false);
        //Time.timeScale = 0;
        _mainCamera = Camera.main;
        SetUpScene();
    }

	void Update () {
        /*if (Input.touchCount > 0)
        {
            Debug.Log("Touchcount number is  "+Input.touchCount);
        }*/
        foreach(Touch touch in Input.touches)
        {

            Ray ray = _mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;
            //Debug.Log("Succssfully create Ray");
            if(Physics.Raycast(ray,out hit))
            {
                GameObject recipient = hit.transform.gameObject;
                Debug.Log("Object tag" + recipient.tag);
                if(touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Touch position is" + touch.position.ToString());
                    if(recipient.CompareTag("Tbutton"))
                    {
                        Debug.Log("Tbutton is hitted.");
                        recipient.SendMessage("Touched");
                    }
                    if(recipient.CompareTag("Sbutton"))
                    {
                        recipient.SendMessage("Touched");
                    }
                }
                if (touch.phase == TouchPhase.Stationary)
                {
                    //Debug.Log("Touch position is" + touch.position.ToString());
                    if (recipient.CompareTag("Tbutton"))
                    {
                        recipient.SendMessage("Touched");
                    }
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    if (recipient.CompareTag("Tbutton"))
                    {
                        Debug.Log("Tbutton is ended.");
                        recipient.SendMessage("leave");
                    }

                }
            }
        }
        /*for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag.CompareTo("Tbutton") == 0)
                    {
                        hit.transform.gameObject.SendMessage("Touched");
                    }
                    if (hit.transform.gameObject.tag.CompareTo("Sbutton") == 0)
                    {
                        hit.transform.gameObject.SendMessage("Touched");
                    }
                }
            }
        }*/
    }


    void SetUpScene() {

        /*Instantiate(ScoreBoardPrefab, _mainCamera.ViewportToWorldPoint(ScoreBoardPosition), Quaternion.identity);
        Instantiate(HealthBarPrefab, _mainCamera.ViewportToWorldPoint(HealthBarPosition), Quaternion.identity);
        Instantiate(MenuButtonPrefab, _mainCamera.ViewportToWorldPoint(MenuButtonPosition), Quaternion.identity);*/

    }

    public void DebugScene() {

        Camera debugCamera = this.GetComponent<Camera>();

        /*ScoreBoardDebug.transform.position = debugCamera.ViewportToWorldPoint(ScoreBoardPosition);
        HealthBarDebug.transform.position = debugCamera.ViewportToWorldPoint(HealthBarPosition);
        MenuButtonDebug.transform.position = debugCamera.ViewportToWorldPoint(MenuButtonPosition);*/
    }



}
