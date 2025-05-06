using System.Collections.Generic;
using UnityEngine;

namespace Game.MapPointers
{
    public class PointersModel
    {
        private Dictionary<string, Vector2> _pointers;

        public PointersModel(Dictionary<string, Vector2> pointers)
        {
            _pointers = new Dictionary<string, Vector2>(pointers);
        }

        public Dictionary<string, Vector2> GetPointers()
        {
            return _pointers;
        }
    }
}