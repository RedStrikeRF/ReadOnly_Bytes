using System;
using System.Collections;
using System.Collections.Generic;

using System.Drawing;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IReadOnlyList<byte>
    {
        readonly byte[] receivedList;
        private int hash { get; set; }
        int IReadOnlyCollection<byte>.Count => ((IReadOnlyList<byte>)receivedList).Count;
        public int Length => ((IReadOnlyList<byte>)receivedList).Count;

        public ReadonlyBytes(params byte[] receivedList)
        {
            if (receivedList == null) throw new ArgumentNullException();

            this.receivedList = new byte[receivedList.Length];

            for (int i = 0; i < receivedList.Length; i++)
                this.receivedList[i] = receivedList[i];

            hash = CalculateHashCode();
        }

        public byte this[int index]
        {
            get
            {
                try
                {
                    return ((IReadOnlyList<byte>)receivedList)[index];
                }
                catch (Exception)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        bool Equals(ReadonlyBytes other)
        {
            if (other == null || Length != other.Length)
                return false;

            for (int i = 0; i < Length; i++)
                if (this[i] != other[i])
                    return false;

            return true;
        }

        int CalculateHashCode()
        {
            int hashCode = 322;

            if (receivedList != null)
                foreach (byte number in receivedList)
                    hashCode = unchecked(hashCode * 322 + number.GetHashCode());

            return hashCode;
        }

        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType())
                return false;

            return this.Equals((ReadonlyBytes)other);
        }

        public override int GetHashCode() => hash;

        public override string ToString()
        {
            string output = "[";
            if (receivedList.Length > 0)
            {
                foreach (byte number in receivedList)
                    output += number + ", ";
                output = output.Remove(output.Length - 2);
            }
            return output += "]";
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return ((IReadOnlyList<byte>)receivedList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}