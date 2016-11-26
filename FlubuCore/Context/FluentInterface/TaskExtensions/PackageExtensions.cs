﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlubuCore.Context.FluentInterface.Interfaces;
using FlubuCore.Targeting;
using FlubuCore.Tasks.Packaging;

namespace FlubuCore.Context.FluentInterface.TaskExtensions
{
    public partial class TaskExtensionsFluentInterface
    {
        public ITaskExtensionsFluentInterface DotnetPackage(string zipPath, params string[] folders)
        {
            Target.Target.AddTask(CreateDotnetPackage(zipPath, folders));
            return this;
        }

        public PackageTask CreateDotnetPackage(string zipPrefix, params string[] folders)
        {
            var task = Context.Tasks().PackageTask(null);

            foreach (var folder in folders)
            {
                var fullFolder = Path.Combine(folder, "bin/Debug/netcoreapp1.0/publish");

                task.AddDirectoryToPackage(
                    folder.GetHashCode().ToString(),
                    fullFolder,
                    Path.GetFileName(folder),
                    true);
            }

            return task;
        }
    }
}
