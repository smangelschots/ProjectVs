# ProjectVs
Start

Add Files to your visual studio project with C#

## Installation

https://www.nuget.org/packages/ProjectVs/

## Features
 
- add files to a visual studio project

## Docs & Community

## Quick Start

Create a unit test project
Add nuget package
```bash  
PM> Install-Package ProjectVs 
```
Create a class library project called vs to your solution
Open the folder of the vs project
Add two files called vs1.cs and vs1.child.cs
Go back to your unit test and in the unittest copy this code
```bash  
var project = new VisualStudioProject();
project.ProjectName = "vs";
project.ProjectPath = @"[replace with your projectpath]";

project.AddClass("vs1.cs");
project.AddClass("vs1.child.cs", "vs1.cs");

project.Save();
```


