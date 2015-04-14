using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FluentInterfaceExample.Types;

namespace FluentInterfaceExample
{
    /// <summary>
    /// Helper class to store our static strings and helper methods.
    /// </summary>
    public static class CommonHelper
    {
        #region String Helpers

        /// <summary>
        /// Words used for enemy first names.
        /// </summary>
        private static readonly string[] _firstNames =
        {
            "Destro",
            "Victo",
            "Mozri",
            "Fang",
            "Ovi",
            "Hell",
            "Syth",
            "End"
        };

        /// <summary>
        /// Words used for enemy last names.
        /// </summary>
        private static readonly string[] _lastNames =
        {
            "math",
            "rin",
            "sith",
            "icous",
            "ravage",
            "wrath",
            "ryn",
            "less"
        };



        /// <summary>
        /// Generates a random name from the static string lists.
        /// </summary>
        /// <returns>Random name</returns>
        public static string GenerateRandomName()
        {
            string result = "";

            System.Random ran = new System.Random((int)DateTime.Now.Ticks);
            result = _firstNames[ran.Next(_firstNames.Length)] + _lastNames[ran.Next(_lastNames.Length)];

            return result;
        }

        #endregion

        /// <summary>
        /// Simple suspense text.
        /// </summary>
        /// <param name="hero">Character</param>
        /// <param name="enemy">Character</param>
        public static void DisplayStartOfBattle(Character hero, Character enemy)
        {
            // Size up the battle statistics.
            Debug.LogWarning("=== Starting Battle ===");
            Debug.Log(hero.ToString() + "vs " + enemy.ToString());

            // Add suspense.
            Debug.Log("An enemy approaches> ");
            //Console.ReadKey();
            //Debug.Log();
            //Debug.Log();
        }


    }
}
