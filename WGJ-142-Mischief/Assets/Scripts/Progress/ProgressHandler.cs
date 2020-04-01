using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Assets.Scripts
{
    public class ProgressHandler : MonoBehaviour
    {

        LevelDifficultyInfo[,] levelsInfo;
        int[,] scoreLog;   ///logs the score achived on each level
        public string LevelsInfoRoot;

        readonly Dictionary<AwarenessState, float> awarnessWeight = new Dictionary<AwarenessState, float>
        {{AwarenessState.UNAWARE,1 },{AwarenessState.SUSPECTING,-1 },{AwarenessState.AWARE,-2 } };

        int currentLevel, currentNeigh;
        Timer timer;
        int giftsCollected;
        Dictionary<AwarenessState, int> enemiesAlerted;

        public ProgressHandler(String LevelsInfoRoot)
        {
            this.LevelsInfoRoot = LevelsInfoRoot;
            this.timer = new Timer();
            this.levelsInfo = DataHandler.ReadFromBinaryFile<LevelDifficultyInfo[,]>(LevelsInfoRoot);
            this.enemiesAlerted = new Dictionary<AwarenessState, int>();
            scoreLog = new int[levelsInfo.GetLength(0), levelsInfo.GetLength(1)];
            //Debug.Log(this.levelsInfo[0, 0].bestTime);
        }

        /// <summary>
        /// This method is to be called at the beginning of each level
        /// </summary>
        /// <param name="levelNum"></param>
        public void StartLevel(int levelNum, int neighNum)
        {
            currentLevel = levelNum;
            currentNeigh = neighNum;
            timer.StartTimer();
            giftsCollected = 0;
            foreach (AwarenessState keys in enemiesAlerted.Keys)
                enemiesAlerted[keys] = 0;
        }

        /// <summary>
        /// When alerting an enemy to a certin alertness level,call this method to record
        /// the action, and punish the users score
        /// </summary>
        /// <param name="alertLevel">Level of alertness the player has alerted an enemy to</param>
        ///<param name="prevAlertLevel">Previous level of alertness the enemy was at</param>
        public void EnemyAlerted(AwarenessState prevAlertLevel, AwarenessState alertLevel)
        {
            if (awarnessWeight[alertLevel] > awarnessWeight[prevAlertLevel]) ///if the new alertness level is 'better' then the old one
                return; //////we wont record it, because we only record the worst
            enemiesAlerted[prevAlertLevel]--;
            enemiesAlerted[alertLevel]++;
        }
        /// <summary>
        /// Call this method when a gift is collected, to update the level score
        /// </summary>
        public void GiftCollected()
        {
            giftsCollected++;
        }

        /// <summary>
        /// returns the number of starts the player has earned for the level
        /// </summary>
        /// <returns>number of half-stars,from 0-6</returns>
        public int GetLevelScore()
        {
            LevelDifficultyInfo info = levelsInfo[currentNeigh, currentLevel];
            float score = 0, stealthScore = 0,
            timeScore = timer.FinishTimer() / info.bestTime, ///precentage of the best time achived by the user
            giftScore = giftsCollected / info.numOfGifts;

            if (giftScore == 0) return 0;    ///no participation prizes in this game

            foreach (AwarenessState state in enemiesAlerted.Keys)
                stealthScore += enemiesAlerted[state] * awarnessWeight[state];
            stealthScore /= info.numOfEnemies;

            score = timeScore + giftScore + stealthScore;
            if (score > 6.0f) score = 6.0f;
            scoreLog[currentNeigh, currentLevel] = Mathf.RoundToInt(score);
            return Mathf.RoundToInt(score);
        }

        /// <summary>
        /// Returns a log of the users progress in the game so far
        /// </summary>
        /// <returns>The games score log so far</returns>
        public int[,] GetScoreLog()
        {
            return scoreLog;
        }
        void Awake()
        {
            DontDestroyOnLoad(this);//Make this a static class that wont delete on switching scenes

            string m_Path = Application.dataPath;
            m_Path += "/Progress/ProgressData.bin";//get the relative path to the binary file
            ProgressHandler p = new ProgressHandler(m_Path);
        }
        void Start()
        {

        }
        void CreateLevelsInfoArray(string m_Path)
        {
            LevelDifficultyInfo[,] data = new LevelDifficultyInfo[,]
           { { new LevelDifficultyInfo(12, 3, 4, 6f),new LevelDifficultyInfo(1, 2, 3, 3f) },
            {new LevelDifficultyInfo(5, 2, 9, 1f),new LevelDifficultyInfo(4, 3, 8, 8f) } };

            DataHandler.WriteToBinaryFile<LevelDifficultyInfo[,]>(m_Path, data);
        }
    }


    [Serializable]
    public class LevelDifficultyInfo
    {
        public readonly int bestTime, numOfGifts, numOfEnemies;
        public readonly float difficultyCoef;

        public LevelDifficultyInfo()
        {

        }

        public LevelDifficultyInfo(int bestTime, int numOfGifts, int numOfEnemies, float difficultyCoef)
        {
            this.bestTime = bestTime;
            this.numOfGifts = numOfGifts;
            this.numOfEnemies = numOfEnemies;
            this.difficultyCoef = difficultyCoef;
        }
        public override string ToString()
        {
            return "bestTime:" + bestTime + ",numOfGifts:" + numOfGifts
                + ",numOfEnemies:" + numOfEnemies + ",coef" + difficultyCoef;
        }
    }

    public class Timer
    {
        float startTime;

        public Timer()
        {

        }
        public void StartTimer()
        {
            startTime = Time.time;
        }
        /// <summary>
        /// Returns the amount of time that passed since 'StartTimer' was called, in seconds
        /// </summary>
        /// <returns>Time since 'StartTimer' was called,in seconds</returns>
        public int FinishTimer()
        {
            return (int)(Time.time - startTime);
        }
    }

}