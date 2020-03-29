using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProgressHandler
    {
        LevelDifficultyInfo[,] levelsInfo;
        int[,] scoreLog;   ///logs the score achived on each level
        readonly Dictionary<AwarenessState, float> awarnessWeight = new Dictionary<AwarenessState, float>
        {{AwarenessState.UNAWARE,1 },{AwarenessState.SUSPECTING,-1 },{AwarenessState.AWARE,-2 } };

        int currentLevel, currentNeigh;
        Timer timer;
        int giftsCollected;
        Dictionary<AwarenessState, int> enemiesAlerted;

        public ProgressHandler(LevelDifficultyInfo[,] levelsInfo)
        {
            this.timer = new Timer();
            this.levelsInfo = levelsInfo;
            this.enemiesAlerted = new Dictionary<AwarenessState, int>();
            scoreLog = new int[levelsInfo.GetLength(0), levelsInfo.GetLength(1)];
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
            enemiesAlerted = new Dictionary<AwarenessState, int>();
        }

        /// <summary>
        /// When alerting an enemy to a certin alertness level,call this method to record
        /// the action, and punish the users score
        /// </summary>
        /// <param name="alertLevel">Level of alertness the player has alerted an enemy to</param>
        public void EnemyAlerted(AwarenessState alertLevel)
        {
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

            if(giftScore == 0) return 0;    ///no participation prizes in this game

            foreach(AwarenessState state in enemiesAlerted.Keys)
                stealthScore += enemiesAlerted[state] * awarnessWeight[state];
            stealthScore /= info.numOfEnemies;

            score = timeScore + giftScore + stealthScore;
            if(score > 6.0f) score = 6.0f;
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
    }

    [Serializable]
    public class LevelDifficultyInfo
    {
        public readonly int bestTime, numOfGifts, numOfEnemies;
        public readonly float difficultyCoef;

        public LevelDifficultyInfo(int bestTime, int numOfGifts, int numOfEnemies, float difficultyCoef)
        {
            this.bestTime = bestTime;
            this.numOfGifts = numOfGifts;
            this.numOfEnemies = numOfEnemies;
            this.difficultyCoef = difficultyCoef;
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