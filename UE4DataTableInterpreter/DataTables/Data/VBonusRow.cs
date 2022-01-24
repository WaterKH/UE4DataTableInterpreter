using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UE4DataTableInterpreter.DataTables.Data
{
    public class VBonusRow
    {
        public long BonusName { get; set; }
        public long BonusEnumProperty { get; set; }
        public long Unk1 { get; set; }
        public long BonusKind { get; set; }
        public byte Unk2 { get; set; }
        public long ETresVictoryBonusKind { get; set; }

        public long AbilityName { get; set; }
        public long AbilityEnumProperty { get; set; }
        public long Unk3 { get; set; }
        public long AbilityType { get; set; }
        public byte Unk4 { get; set; }
        public long ETresVictoryAbilityType { get; set; }

        public VBonusRow Decompile(FileStream reader)
        {
            this.BonusName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BonusEnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BonusKind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = (byte)reader.ReadByte();
            this.ETresVictoryBonusKind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            
            this.AbilityName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AbilityEnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AbilityType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = (byte)reader.ReadByte();
            this.ETresVictoryAbilityType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.BonusName));
            data.AddRange(BitConverter.GetBytes(this.BonusEnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.BonusKind));
            data.Add(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.ETresVictoryBonusKind));

            data.AddRange(BitConverter.GetBytes(this.AbilityName));
            data.AddRange(BitConverter.GetBytes(this.AbilityEnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.AbilityType));
            data.Add(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.ETresVictoryAbilityType));

            return data;
        }
    }
}