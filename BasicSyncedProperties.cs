using UnityEngine;

namespace WreckMP_Ref
{
    public class SyncedFloat : SyncedProperty<float>
    {
        public SyncedFloat(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the difference is bigger than this value the event is sent.
        /// </summary>
        public float tolerance = 0f;
        float lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough
        protected override bool IsChanged(float oldVal, float newVal)
        {
            float diff = lastUpdatedValue - newVal;
            if (diff < 0) diff = -diff;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadSingle();
        }
    }

    public class SyncedDouble : SyncedProperty<double>
    {
        public SyncedDouble(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the difference is bigger than this value the event is sent.
        /// </summary>
        public double tolerance = 0;
        double lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(double oldVal, double newVal)
        {
            double diff = lastUpdatedValue - newVal;
            if (diff < 0) diff = -diff;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadDouble();
        }
    }

    public class SyncedInt16 : SyncedProperty<short>
    {
        public SyncedInt16(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the difference is bigger than this value the event is sent.
        /// </summary>
        public short tolerance = 0;
        short lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(short oldVal, short newVal)
        {
            int diff = lastUpdatedValue - newVal;
            if (diff < 0) diff = -diff;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadInt16();
        }
    }

    public class SyncedInt32 : SyncedProperty<int>
    {
        public SyncedInt32(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the difference is bigger than this value the event is sent.
        /// </summary>
        public int tolerance = 0;
        int lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(int oldVal, int newVal)
        {
            int diff = lastUpdatedValue - newVal;
            if (diff < 0) diff = -diff;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadInt32();
        }
    }

    public class SyncedInt64 : SyncedProperty<long>
    {
        public SyncedInt64(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the difference is bigger than this value the event is sent.
        /// </summary>
        public long tolerance = 0;
        long lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(long oldVal, long newVal)
        {
            long diff = lastUpdatedValue - newVal;
            if (diff < 0) diff = -diff;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadInt64();
        }
    }

    public class SyncedBoolean : SyncedProperty<bool>
    {
        public SyncedBoolean(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        protected override bool IsChanged(bool oldVal, bool newVal)
        {
            return oldVal != newVal;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadBoolean();
        }
    }

    public class SyncedString : SyncedProperty<string>
    {
        public SyncedString(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        protected override bool IsChanged(string oldVal, string newVal)
        {
            return oldVal != newVal;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            if (_value == null) return;
            p.Write(_value);
        }

        protected override void Read(GameEventReader p)
        {
            if (p.UnreadLength() == 0)
            {
                _value = null;
                return;
            }
            _value = p.ReadString();
        }
    }

    public class SyncedVector2 : SyncedProperty<Vector2>
    {
        public SyncedVector2(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the magnitude difference is bigger than this value the event is sent.
        /// </summary>
        public float tolerance = 0f;
        Vector2 lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(Vector2 oldVal, Vector2 newVal)
        {
            var diff = lastUpdatedValue - newVal;
            return diff.sqrMagnitude > (tolerance * tolerance);
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value.x);
            p.Write(_value.y);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            float x = p.ReadSingle();
            float y = p.ReadSingle();
            _value = new Vector2(x, y);
        }
    }

    public class SyncedVector3 : SyncedProperty<Vector3>
    {
        public SyncedVector3(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the magnitude difference is bigger than this value the event is sent.
        /// </summary>
        public float tolerance = 0f;
        Vector3 lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(Vector3 oldVal, Vector3 newVal)
        {
            var diff = lastUpdatedValue - newVal;
            return diff.sqrMagnitude > (tolerance * tolerance);
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            _value = p.ReadVector3();
        }
    }

    public class SyncedColor : SyncedProperty<Color>
    {
        public SyncedColor(string uid, bool registerTimer = false) : base(uid, registerTimer) { }

        /// <summary>
        /// When new value is set, if the sum of differences of all channels is bigger than this value the event is sent.
        /// </summary>
        public float tolerance = 0f;
        Color lastUpdatedValue;

        // Last updated value is technically oldVal, but when it was last sent
        // So if value is gradually increased by small steps, it will be updated when the change is big enough

        protected override bool IsChanged(Color oldVal, Color newVal)
        {
            float dr = lastUpdatedValue.r - newVal.r;
            if (dr < 0) dr = -dr;
            float dg = lastUpdatedValue.g - newVal.g;
            if (dg < 0) dg = -dg;
            float db = lastUpdatedValue.b - newVal.b;
            if (db < 0) db = -db;
            float da = lastUpdatedValue.a - newVal.a;
            if (da < 0) da = -da;

            float diff = dr + dg + db + da;
            return diff > tolerance;
        }

        protected override void Write(GameEventWriter p, bool initial)
        {
            p.Write(_value.r);
            p.Write(_value.g);
            p.Write(_value.b);
            p.Write(_value.a);
            lastUpdatedValue = _value;
        }

        protected override void Read(GameEventReader p)
        {
            float r = p.ReadSingle();
            float g = p.ReadSingle();
            float b = p.ReadSingle();
            float a = p.ReadSingle();
            _value = new Color(r, g, b, a);
        }
    }
}
