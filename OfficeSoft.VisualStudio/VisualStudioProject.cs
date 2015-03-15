using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;
using OfficeSoft.Logging;

namespace OfficeSoft.VisualStudio
{
    public enum ProjectType
    {
        MVC
    }

    public class VisualStudioProject : BaseProject
    {
        public VisualStudioProject()
        {
            this.AddedItems = new List<ProjectItem>();

        }

        private Project Project { get; set; }
        private List<ProjectItem> AddedItems { get; set; }


        public ProjectType ProjectType { get; set; }

        public Project GetProject()
        {
            if (Project != null)
            {
                return Project;
            }
            if (string.IsNullOrEmpty(ProjectPath))
            {
                Log.Add(new LogModel()
                {
                    FunctionsName = "GetProject",
                    LogType = LogType.Warning,
                    Message = "Project path is missing"
                });
                return null;
            }
            if (string.IsNullOrEmpty(ProjectName)) { return null; }

            var info = new DirectoryInfo(ProjectPath);
            FileInfo[] files = info.GetFiles(string.Format("{0}*", ProjectName), SearchOption.TopDirectoryOnly);
            if (files.Any())
            {
                FileInfo projectFile = files.FirstOrDefault(fileInfo => fileInfo.Extension.Contains(".csproj"));

                if (projectFile != null)
                {
                    var project = new Project(projectFile.FullName);
                    return project;
                }
            }

            return null;
        }

        public void AddClass(string fileName, string dependentUpon = "")
        {
            AddNewItem("Compile", fileName, dependentUpon);
        }


        public void AddNewItem(string itemName, string fileName, string dependentUpon = "")
        {
            Project = GetProject();
            if (Project == null)
                return;



            Project.DisableMarkDirty = true;
            var items = Project.GetItems(itemName);
            foreach (var itemlookup in items)
            {
                if (itemlookup.EvaluatedInclude == fileName)
                {
                    return;
                }
            }
            var metadata = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(dependentUpon))
            {
                metadata.Add(new KeyValuePair<string, string>("DependentUpon", dependentUpon));
            }
            var buildItem = Project.AddItem(itemName, fileName, metadata).SingleOrDefault();
            AddedItems.Add(buildItem);
        }

        public void Save()
        {
            if (Project == null) return;


            Project.DisableMarkDirty = false;
            if (AddedItems.Any())
                Project.Save();
        }

        public static LogModelCollection Log = new LogModelCollection();

    }
}
