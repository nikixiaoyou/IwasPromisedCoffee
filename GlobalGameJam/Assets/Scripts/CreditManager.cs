using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{

	public float offsetBetweenCredit = 5f;
	public float speed = 5f;

	public float positionYCheck = 30f;
	public float positionYReset = -30f;

	private List<Transform> listChild = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
		for(int i =0; i < this.transform.childCount; i++)
		{
			listChild.Add(this.transform.GetChild(i));
		}

		listChild[0].localPosition = new Vector3(listChild[0].localPosition.x, -10, listChild[0].localPosition.z);

		for(int i =1; i < this.transform.childCount; i++)
		{
			listChild[i].localPosition = new Vector3(listChild[i].localPosition.x, listChild[i-1].localPosition.y - offsetBetweenCredit, listChild[i].localPosition.z);
		}


	}

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < this.transform.childCount; i++)
		{
			listChild[i].localPosition = new Vector3(listChild[i].localPosition.x, listChild[i].localPosition.y  +  speed * Time.deltaTime, listChild[i].localPosition.z);

			if (listChild[i].localPosition.y >= positionYCheck)
			{
				listChild[i].localPosition = new Vector3(listChild[i].localPosition.x, positionYReset, listChild[i].localPosition.z);
			}
		}
	}
}
