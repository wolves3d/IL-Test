using UnityEngine;
using System.Collections.Generic;

public class RankSceneData
{
	public class LeaderInfo
	{
		public LeaderInfo(string n, int s, int status)
		{
			name = n;
			score = s;

			// fake
			statusID = status;
			modeID = Random.Range(0, 2);
		}

		public string name;
		public int score;
		public int statusID;
		public int modeID;
	};

	public bool IsSet()
	{
		return (null != m_leaderList);
	}

	public void SetJsonData(string jsonString)
	{
		// Clear data (if any) -------------------------------------------------

		if (null != m_leaderList)
		{
			m_leaderList.Clear();
		}

		if (null == jsonString)
		{
			// Fake data -------------------------------------------------------

			if (null == m_leaderList)
			{
				m_leaderList = new List<LeaderInfo>();
			}

			m_leaderList.Add(new LeaderInfo("Jon Snow", 65535, 0));
			m_leaderList.Add(new LeaderInfo("Eddard Stark", 65000, 1));
			m_leaderList.Add(new LeaderInfo("Khaleesi", 62100, 0));
			m_leaderList.Add(new LeaderInfo("Tyrion Lannister", 60999, 1));
			m_leaderList.Add(new LeaderInfo("Melisandre", 53200, 0));
			m_leaderList.Add(new LeaderInfo("Cersei Lannister", 25000, 1));
			m_leaderList.Add(new LeaderInfo("Brienne of Tarth", 18000, 0));
			m_leaderList.Add(new LeaderInfo("Joffrey", 13000, 1));
			m_leaderList.Add(new LeaderInfo("Drogo", 5000, 0));
			m_leaderList.Add(new LeaderInfo("Jon Arryn", 1980, 1));
			m_leaderList.Add(new LeaderInfo("Theon Greyjoy", 800, 0));

			RefreshLeaderList();
			//m_leaderCount = Random.Range(1, m_leaderList.Count);

			// user info

			int randomID = Random.Range(0, (m_leaderList.Count - 1));
			LeaderInfo info = m_leaderList[randomID];

			m_userName = info.name;
			m_userScore = info.score;
			m_userRank = Random.Range(1024, 65536);
		}
		else
		{
			// TODO: parse data here -------------------------------------------
			Debug.Assert(false, "Implement me! Parse JSON here");
		}

		RefreshLeaderList();
	}

	public void SetFilter(int mode)
	{
		m_modeID = mode;
		RefreshLeaderList();
	}

	public void SetStatus(int statusID)
	{
		m_statusID = statusID;
		RefreshLeaderList();
	}

	public void RefreshLeaderList()
	{
		if (null == m_leaderList)
		{
			return;
		}


		if (null == m_leaderListFiltered)
		{
			m_leaderListFiltered = new List<LeaderInfo>();
		}
		else
		{
			m_leaderListFiltered.Clear();
		}

		for (int i = 0; i < m_leaderList.Count; ++i)
		{
			LeaderInfo info = m_leaderList[i];

			if (-1 != m_statusID)
			{
				if (info.statusID != m_statusID)
				{
					// status not match
					continue;
				}
			}

			if (info.modeID != m_modeID)
			{
				// difficulty mode not match
				continue;
			}

			// FIXME: change to index numbers (not objects!)
			m_leaderListFiltered.Add(info);
		}
	}

	public int LeadersCount()
	{
		//return m_leaderCount;
		return m_leaderListFiltered.Count;
	}

	public void FillLeaderView(RankUserInfoView targetView, int leaderID)
	{
		LeaderInfo info = m_leaderListFiltered[leaderID];
		targetView.SetValues((leaderID + 1), info.name, info.score);
	}

	public string GetUserName()
	{
		return m_userName;
	}

	public int GetUserRank()
	{
		return m_userRank;
	}

	public int GetUserScore()
	{
		return m_userScore;
	}

	string m_userName;	
	int	m_userRank;
	int m_userScore;

	int m_modeID;
	int m_statusID;

	List<LeaderInfo> m_leaderList;
	List<LeaderInfo> m_leaderListFiltered;
};