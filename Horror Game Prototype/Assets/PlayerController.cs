using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMotor Player3D;
    public Joystick joystick;
    private Motor2D player2D;

    private Zone currentZone;
    public Zone CurrentZone
    {
        get { return currentZone; }
        private set { }
    }

    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get { return _instance; }
        private set { }
    }

    void Awake()
    {
        _instance = this;
        currentZone = Zone.NoZone;
    }

    public void Set2DMotor(Motor2D newPlayer)
    {
        player2D = newPlayer;
    }

    public void SetZone(Zone newZone)
    {
        currentZone = newZone;
    }

    void Update()
    {
        float delta = Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Player3D.CameraMovement(mouseX, mouseY);

        float horizontal = Input.GetAxis("Horizontal");
        joystick.GetInputs(horizontal);

        if (player2D != null && currentZone == Zone.TVZone)
        {
            
            player2D.GetInputs(horizontal);
            player2D.Tick(delta);
        }
    }

  
}
public enum Zone
{
    NoZone,
    TVZone
}