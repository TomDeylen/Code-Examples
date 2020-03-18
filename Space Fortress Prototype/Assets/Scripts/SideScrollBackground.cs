using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollBackground : MonoBehaviour
{
    public float xScroll = 2f;
    public float yScroll = 0f;
    public EnvironmentMove[] environments;

    void Update()
    {
        for (int i = 0; i < environments.Length; i++)
        {
            environments[i].Scroll(xScroll, yScroll);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelDirectionChange changeDir = collision.transform.GetComponent<LevelDirectionChange>();
        if(changeDir != null)
        {
            xScroll = changeDir.newX;
            yScroll = changeDir.newY;
        }
    }

}

[System.Serializable]
public class EnvironmentMove
{
    public float xDiff = 0;
    public float yDiff = 0;
    public Transform[] environment;

    public void Scroll(float xScroll, float yScroll)
    {
        Vector3 scroll = new Vector3(-xScroll - xDiff, -yScroll - yDiff, 0 ) * 0.1f * Time.deltaTime;
        for (int i = 0; i < environment.Length; i++)
        {
            environment[i].position += scroll;
        }
        
    }
}