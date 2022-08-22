using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UE4DataTableInterpreter.Models
{
    public class Enemy
    {
        public string FilePath { get; set; }
        public string[] Addresses { get; set; }
        public string EnemyPath { get; set; }
        public bool SpawnOriginal { get; set; }
        public string Key { get; set; }
    }
}
