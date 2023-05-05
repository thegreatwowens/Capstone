using UnityEngine;
using OwnCode;


[RequireComponent(typeof(PanelManager))]
public class PanelManagerOveride : MonoBehaviour
{
    PanelManager panel;
    public bool StartOnactive;
    public bool interactable;
    public bool blockRaycast;
    private void Awake()
    {
        panel = this.gameObject.GetComponent<PanelManager>();
    }
    private void Start()
    {
        if (StartOnactive)
        {
            panel.OpenPanel();

        }
        else
        {
            return;

        }
    }
    private void Update()
    {
    }



}
