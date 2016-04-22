using UnityEngine;
using System.Collections;

public class rankScene : MonoBehaviour {

	public GameObject leaderLayout;
	public GameObject rankUserInfoPrefab;

	// Use this for initialization
	void Start ()
	{
		//GameObject testPrefab = Resources.Load<GameObject>("rankUserInfo");

		for (uint i = 0; i < 10; ++i)
		{
			GameObject test = Instantiate(rankUserInfoPrefab);
			test.SetActive(true);
			test.transform.SetParent(leaderLayout.transform);
			test.transform.localPosition = Vector3.zero;
			test.transform.localScale = Vector3.one;

			RankUserInfoView rankView = test.GetComponent<RankUserInfoView>();
			rankView.SetValues((i + 1), "wolves3d", (i + 1) * 10000);

			//test.getChil
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
