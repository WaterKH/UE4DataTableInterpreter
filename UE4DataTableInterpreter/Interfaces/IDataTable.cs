using System.IO;

namespace UE4DataTableInterpreter.Interfaces
{
    public interface IDataTable
    {
        public int IndexId { get; set; }
        public string RowName { get; set; }

        public IDataTable Decompile(FileStream reader);
    }
}