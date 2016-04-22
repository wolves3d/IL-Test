using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class rankScene : MonoBehaviour {

	public GameObject rankUserInfoPrefab;

	public GameObject leaderLayout;
	public GameObject leadersBG;

	public Image allStatusImage;
	public Image onlyStatusImage;
	public Sprite uncheckedStatusTab;
	public Sprite checkedStatusTab;

	public Text userNameObject;
	public Text userRankObject;
	public Text userScoreObject;

	// Use this for initialization
	void Start ()
	{
		m_rankData = new RankSceneData();
		m_leaderViewList = new List <RankUserInfoView>();

		// Set defaults
		m_rankData.SetFilter(1);
		OnAllStatusTab();

		// Fake server response emulation
		OnServerResponse(null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnServerResponse(string jsonString)
	{
		m_rankData.SetJsonData(jsonString);
		UpdateViews();
	}

	void UpdateViews()
	{
		if (false == m_rankData.IsSet())
		{
			return;
		}

		// User ----------------------------------------------------------------
		userNameObject.text = m_rankData.GetUserName();
		userRankObject.text = m_rankData.GetUserRank().ToString();
		userScoreObject.text = m_rankData.GetUserScore().ToString();

		// Leaders -------------------------------------------------------------
		int leaderCount = m_rankData.LeadersCount();

		for (int i = 0; i < leaderCount; ++i)
		{
			RankUserInfoView rankView = null;

			if (m_leaderViewList.Count <= i)
			{
				// Create new rankView
				GameObject rankObject = Instantiate(rankUserInfoPrefab);
				rankObject.SetActive(true);
				rankObject.transform.SetParent(leaderLayout.transform);
				rankObject.transform.localPosition = Vector3.zero;
				rankObject.transform.localScale = Vector3.one;

				rankView = rankObject.GetComponent<RankUserInfoView>();
				m_leaderViewList.Add(rankView);
			}
			else
			{
				// Use existing
				rankView = m_leaderViewList[i];
			}

			m_rankData.FillLeaderView(rankView, i);
		}

		// Remove unused views (if any) ----------------------------------------
		if (leaderCount < m_leaderViewList.Count)
		{
			for (int i = leaderCount; i < m_leaderViewList.Count; ++i)
			{
				GameObject view = m_leaderViewList[i].gameObject;
				Destroy(view);
			}

			m_leaderViewList.RemoveRange(leaderCount,
				m_leaderViewList.Count - leaderCount);
		}
		

		// Change background height according to leadersCount & leadersLayout spacing
		UnityEngine.UI.VerticalLayoutGroup layout = leaderLayout.GetComponent<UnityEngine.UI.VerticalLayoutGroup>();
		RectTransform rt = leadersBG.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, 20 + (leaderCount * layout.spacing));

		// FIXME: change scroll content size too!
	}

	public void OnBackButton()
	{
		// fake code
		OnServerResponse(null);
	}

	public void OnShareButton()
	{
		
	}

	public void OnAllStatusTab()
	{
		allStatusImage.sprite = checkedStatusTab;
		onlyStatusImage.sprite = uncheckedStatusTab;

		m_rankData.SetStatus(-1);
		UpdateViews();
	}

	public void OnOnlyTab()
	{
		onlyStatusImage.sprite = checkedStatusTab;
		allStatusImage.sprite = uncheckedStatusTab;

		m_rankData.SetStatus(1);
		UpdateViews();
	}

	RankSceneData m_rankData;
	List <RankUserInfoView> m_leaderViewList;
}
