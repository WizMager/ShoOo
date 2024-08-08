﻿namespace Utils.LayerMask
{
    public class LayerMasks
    {
        public static int Enemy => EnemyMask.Value;
        public static int DroppedWeapon => DroppedWeaponMask.Value;
        
        private static readonly Mask EnemyMask = new(Layers.Enemy);
        private static readonly Mask DroppedWeaponMask = new(Layers.DroppedWeapon);
        
        private class Mask
        {
            private readonly string[] _layerNames;

            private int? _value;

            public Mask(params string[] layerNames)
            {
                _layerNames = layerNames;
            }

            public int Value
            {
                get
                {
                    _value ??= UnityEngine.LayerMask.GetMask(_layerNames);
                    
                    return _value.Value;
                }
            }
        }
    }
}