
namespace leaderboardModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    using UnityEngine;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;




    [System.Serializable]
    public class allTimeLEaderBoard
    {
        public List<assetClass> allTimeLeadboeardClass;
    }
    [System.Serializable]
    public class assetClass
    {
        public string _id;
        public int id;
        public int dailyScore;
        public int allTimeScore;
        public int score;
        public int weeklyScore;
        public int tournamentScore;
        public int longestDistance;
        public bool tournamentStatus;
        public int dailySessionPlayed;
        public int totalSessionPlayed;
        public DateTime updatedAt;
        public int __v;
        public string name;
    }
    [System.Serializable]
    public class userPostedData
    {
        public int id;
        public int score;
    }
    [System.Serializable]
    public class userGetDataModel
    {
        public string id;
    }
    [System.Serializable]
    public class tournamentLeaderboardClass
    {
        public string id;
        public int score;
    }
    [System.Serializable]
    public class tournamentClass
    {
        public bool status;
        public string name;
        public string guild;
        public tournamentLeaderboardClass[] leaderboard;
    }
    public class userGetTournamentDataModel
    {
        public string id;
        public string game;
    }

    public class userGetDataModelSecond
    {
        public int userid;
    }

}



