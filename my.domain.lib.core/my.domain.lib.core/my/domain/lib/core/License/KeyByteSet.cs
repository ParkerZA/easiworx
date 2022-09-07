// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.License.KeyByteSet
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

namespace my.domain.lib.core.License
{
    public class KeyByteSet
    {
        private byte[] _bytes;

        public KeyByteSet(int keyByteNo, byte keyByteA, byte keyByteB, byte keyByteC)
        {
            this.KeyByteNo = keyByteNo;
            this.KeyByteA = keyByteA;
            this.KeyByteB = keyByteB;
            this.KeyByteC = keyByteC;
        }

        public int KeyByteNo { get; private set; }

        public byte KeyByteA { get; private set; }

        public byte KeyByteB { get; private set; }

        public byte KeyByteC { get; private set; }

        public KeyByteSet(int keyByteNo, byte[] bytes)
        {
            this.KeyByteNo = keyByteNo;
            this._bytes = bytes;
        }

        public byte[] KeyBytes
        {
            get
            {
                return this._bytes;
            }
        }
    }
}
