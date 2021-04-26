using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float t = 0;
    float lastTime = 0;
    public static float velocidade = 5;
    float firstPos = 0;
    float objX;
    float objY;
    float newY;
    float newX;

    public Material ShoulderMaterial;
    public Material ElbowMaterial;
    public Material HandMaterial;

    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        int dist = PlayerPrefs.GetInt("Distancia");
        int Restrito = PlayerPrefs.GetInt("Modo");
        int Simetria = PlayerPrefs.GetInt("Simetria");

        objX = transform.position.x;
        objY = transform.position.y;

        if (dist == 0) //pequena
        {
            if (objX > 1.2f) newX = objX - 0.7f;
            if (objX < -1.2f) newX = objX + 0.7f;
            else newX = objX;

            if (objY > 1) newY = objY - 0.5f;
            if (objY < -1) newY = objY + 0.5f;
        }
        else if (dist == 1) //media
        {
            if (objY > 0.75f)
                newY = 1;
            else if (objY <= 0.75f && objY >= -0.2f)
                newY = 0;
            else if (objY < -0.2f)
                newY = -1;
            if (objX > 1.7f) newX = objX - 0.3f;
            else if (objX < -1.7f) newX = objX + 0.3f;
            else newX = objX;
        }
        else if (dist == 2) //grande
        {
            if (objY < 1 && objY > 0.5f)  newY = 1.5f;
            else if (objY < -0.5f && objY > -1) newY = -1.5f;
            else newY = objY;

            if (objX > 0.5f && objX < 1.3f) newX = objX + 1;
            else if (objX < -0.5f && objX > -1.3f) newX = objX - 1;
            else newX = objX;
        }

        if (Restrito == 1 && Simetria == 0)
        {
            if (objY >= 1.4f && objY <= 2)
            {
                gameObject.tag = "shoulderObject";
                rend.material = ShoulderMaterial;

            }
            else if (objY < 0.3f && objY > -0.3f)
            {
                gameObject.tag = "elbowObject";
                rend.material = ElbowMaterial;
            }
            else if (objY <= -0.3f || objY >= 0.3f && objY < 1.4f || objY > 2 )
            {
                gameObject.tag = "handObject";
                rend.material = HandMaterial;
            }

        }
        else if (Simetria == 1 && Restrito == 1)
        {
            if (objY >= 1.4f && objY <= 2)
            {
                gameObject.tag = "handObject";
                rend.material = HandMaterial;

            }
            else if (objY < 0.3f && objY > -0.3f)
            {
                gameObject.tag = "elbowObject";
                rend.material = ElbowMaterial;
            }
            else if (objY <= -0.3f || objY >= 0.3f && objY < 1.4f || objY > 2)
            {
                gameObject.tag = "handObject";
                rend.material = HandMaterial;
            }
        }
        transform.position = new Vector3(newX, (newY + 0.6f), transform.position.z);
    }

    void Update ()
    {
		if(t == 0) firstPos = transform.position.z;
		float newpos = getNewValue(t, firstPos);

		if(newpos > -5)
			transform.position = new Vector3(transform.position.x,transform.position.y, newpos);
        t = Time.timeSinceLevelLoad;
    }
    
	public static float getNewValue(float time, float pos)
	{
		float novaPos = pos - (time * velocidade);
		return novaPos;
	}
}
