namespace OfficeSoft.VisualStudio
{
    public class BaseProject 
    {
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }

        public BaseProject()
        {
            
        }

        public BaseProject(string path, string name)
        {
            this.ProjectPath = path;
            this.ProjectName = name;
        }


    }
}
