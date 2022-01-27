/*
MIT License

Copyright (c) 2017 Chris Johnston

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Changes made by Matthew Martin (2022)
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AchievementAPI
{
    public class GamerScoreGenerator
    {
        private readonly long[] scores = { 10, 20, 25, 50, 75, 100, 125, 150, 200, 300, 500 };
        private readonly long[] rareScores = { 100, 125, 150, 200, 250, 300, 350, 500 };
        
        private readonly Random rng;
        private static GamerScoreGenerator _instance;
        public static GamerScoreGenerator Instance
        {
            get
            {
                if(_instance != null)
                {
                    _instance = new GamerScoreGenerator();
                }
                return _instance;
            }
        }
        private GamerScoreGenerator()
        {
            rng = new Random();
        }

        /// <summary>
        /// Returns a randomly-selected gamerscore
        /// </summary>
        /// <returns></returns>
        public long GetGamerScore
            => scores[rng.Next(scores.Length)];

        /// <summary>
        /// Gets a rare gamerscore amount.
        /// </summary>
        /// <returns>Returns an integer of a randomly-selected rare gamerscore</returns>
        public long GetRareGamerScore
            => rareScores[rng.Next(rareScores.Length)];

    }
}
