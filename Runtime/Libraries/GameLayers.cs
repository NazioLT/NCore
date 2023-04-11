using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class GameLayers
    {
        /// <summary>Layer for player shooting</summary>
        public static readonly LayerMask PlayerShootLayer = LayerMask.GetMask("Default") | LayerMask.GetMask("Ennemy");
        /// <summary>Layer for Ennemy shooting</summary>
        public static readonly LayerMask EnnemyShootLayer = LayerMask.GetMask("Default") | LayerMask.GetMask("Player");
        public static readonly LayerMask Environement = LayerMask.GetMask("Default") | LayerMask.GetMask("Floor");
        public static readonly LayerMask Floor = LayerMask.GetMask("Floor");
    }
}