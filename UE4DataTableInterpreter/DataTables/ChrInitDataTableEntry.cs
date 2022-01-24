using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class ChrInitDataTableEntry : IDataTable
    {
        public ChrInitDataTableEntry() { }

        public int IndexId { get { return (int)this.m_PlayerSora.Id; } set { } }
        public string RowName { get; set; }

        public ChrInitRow m_PlayerSora { get; set; }
        //public ChrInitRow m_PlayerSoraTuto { get; set; }
        //public ChrInitRow m_PlayerSoraTSGame { get; set; }
        //public ChrInitRow m_PlayerRiku29 { get; set; }
        //public ChrInitRow m_PlayerAqua { get; set; }
        //public ChrInitRow m_PlayerKairi { get; set; }
        //public ChrInitRow m_PlayerRoxas { get; set; }

        //public ChrInitRow m_FriendSora { get; set; }
        //public ChrInitRow m_FriendDonald { get; set; }
        //public ChrInitRow m_FriendGoofy { get; set; }
        //public ChrInitRow m_FriendRiku { get; set; }
        //public ChrInitRow m_FriendMickey { get; set; }
        //public ChrInitRow m_FriendAqua { get; set; }
        //public ChrInitRow m_FriendJackSparrow { get; set; }
        //public ChrInitRow m_FriendWoody { get; set; }
        //public ChrInitRow m_FriendBuzz { get; set; }
        //public ChrInitRow m_FriendHercules { get; set; }
        //public ChrInitRow m_FriendRapunzel { get; set; }
        //public ChrInitRow m_FriendFlynn { get; set; }
        //public ChrInitRow m_FriendSulley { get; set; }
        //public ChrInitRow m_FriendMike { get; set; }
        //public ChrInitRow m_FriendMarshmallow { get; set; }
        //public ChrInitRow m_FriendBaymax { get; set; }
        //public ChrInitRow m_FriendKairi { get; set; }
        //public ChrInitRow m_FriendTerra { get; set; }
        //public ChrInitRow m_FriendVentus { get; set; }
        //public ChrInitRow m_FriendRoxas { get; set; }
        //public ChrInitRow m_FriendLea { get; set; }
        //public ChrInitRow m_FriendXion { get; set; }
        public List<byte> RestOfData { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.m_PlayerSora = new ChrInitRow().Decompile(reader);
            //this.m_PlayerSoraTuto = new ChrInitRow().Decompile(reader);
            //this.m_PlayerSoraTSGame = new ChrInitRow().Decompile(reader);
            //this.m_PlayerRiku29 = new ChrInitRow().Decompile(reader);
            //this.m_PlayerAqua = new ChrInitRow().Decompile(reader);
            //this.m_PlayerKairi = new ChrInitRow().Decompile(reader);
            //this.m_PlayerRoxas = new ChrInitRow().Decompile(reader);

            //this.m_FriendSora = new ChrInitRow().Decompile(reader);
            //this.m_FriendDonald = new ChrInitRow().Decompile(reader);
            //this.m_FriendGoofy = new ChrInitRow().Decompile(reader);
            //this.m_FriendRiku = new ChrInitRow().Decompile(reader);
            //this.m_FriendMickey = new ChrInitRow().Decompile(reader);
            //this.m_FriendAqua = new ChrInitRow().Decompile(reader);
            //this.m_FriendJackSparrow = new ChrInitRow().Decompile(reader);
            //this.m_FriendWoody = new ChrInitRow().Decompile(reader);
            //this.m_FriendBuzz = new ChrInitRow().Decompile(reader);
            //this.m_FriendHercules = new ChrInitRow().Decompile(reader);
            //this.m_FriendRapunzel = new ChrInitRow().Decompile(reader);
            //this.m_FriendFlynn = new ChrInitRow().Decompile(reader);
            //this.m_FriendSulley = new ChrInitRow().Decompile(reader);
            //this.m_FriendMike = new ChrInitRow().Decompile(reader);
            //this.m_FriendMarshmallow = new ChrInitRow().Decompile(reader);
            //this.m_FriendBaymax = new ChrInitRow().Decompile(reader);
            //this.m_FriendKairi = new ChrInitRow().Decompile(reader);
            //this.m_FriendTerra = new ChrInitRow().Decompile(reader);
            //this.m_FriendVentus = new ChrInitRow().Decompile(reader);
            //this.m_FriendRoxas = new ChrInitRow().Decompile(reader);
            //this.m_FriendLea = new ChrInitRow().Decompile(reader);
            //this.m_FriendXion = new ChrInitRow().Decompile(reader);

            this.RestOfData = reader.ReadBytesFromFileStream((int)(reader.Length - reader.Position));

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(this.m_PlayerSora.Recompile());
            //data.AddRange(this.m_PlayerSoraTuto.Recompile());
            //data.AddRange(this.m_PlayerSoraTSGame.Recompile());
            //data.AddRange(this.m_PlayerRiku29.Recompile());
            //data.AddRange(this.m_PlayerAqua.Recompile());
            //data.AddRange(this.m_PlayerKairi.Recompile());
            //data.AddRange(this.m_PlayerRoxas.Recompile());

            //data.AddRange(this.m_FriendSora.Recompile());
            //data.AddRange(this.m_FriendDonald.Recompile());
            //data.AddRange(this.m_FriendGoofy.Recompile());
            //data.AddRange(this.m_FriendRiku.Recompile());
            //data.AddRange(this.m_FriendMickey.Recompile());
            //data.AddRange(this.m_FriendAqua.Recompile());
            //data.AddRange(this.m_FriendJackSparrow.Recompile());
            //data.AddRange(this.m_FriendWoody.Recompile());
            //data.AddRange(this.m_FriendBuzz.Recompile());
            //data.AddRange(this.m_FriendHercules.Recompile());
            //data.AddRange(this.m_FriendRapunzel.Recompile());
            //data.AddRange(this.m_FriendFlynn.Recompile());
            //data.AddRange(this.m_FriendSulley.Recompile());
            //data.AddRange(this.m_FriendMike.Recompile());
            //data.AddRange(this.m_FriendMarshmallow.Recompile());
            //data.AddRange(this.m_FriendBaymax.Recompile());
            //data.AddRange(this.m_FriendKairi.Recompile());
            //data.AddRange(this.m_FriendTerra.Recompile());
            //data.AddRange(this.m_FriendVentus.Recompile());
            //data.AddRange(this.m_FriendRoxas.Recompile());
            //data.AddRange(this.m_FriendLea.Recompile());
            //data.AddRange(this.m_FriendXion.Recompile());
            data.AddRange(this.RestOfData);

            return data;
        }
    }
}