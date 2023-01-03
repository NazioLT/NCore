using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class GameLayers
    {
        /// <summary>Layer for player shooting</summary>
        public static LayerMask PlayerShootLayer => LayerMask.GetMask("Default") | LayerMask.GetMask("Ennemy");
        /// <summary>Layer for Ennemy shooting</summary>
        public static LayerMask EnnemyShootLayer => LayerMask.GetMask("Default") | LayerMask.GetMask("Player");
        public static LayerMask Environement => LayerMask.GetMask("Default");
    }
}

