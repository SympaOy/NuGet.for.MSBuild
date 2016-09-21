using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    public class CompareItems : Task
    {
        [Required]
        public ITaskItem[] ItemsA { get; set; }

        [Required]
        public ITaskItem[] ItemsB { get; set; }

        [Output]
        public ITaskItem[] Diff { get; set; }

        public override bool Execute()
        {
            List<ITaskItem> result = new List<ITaskItem>();

            result.AddRange(from a in this.ItemsA
                            where !this.ItemsB.Any(b => b.ItemSpec == a.ItemSpec)
                            select a);

            result.AddRange(from b in this.ItemsB
                            where !this.ItemsA.Any(a => a.ItemSpec == b.ItemSpec)
                            select b);

            this.Diff = result.ToArray();

            return true;
        }
    }
}