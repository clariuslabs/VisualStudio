using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Clarius.VisualStudio.Tasks")]
[assembly: AssemblyDescription("Compiled Clarius.VisualStudio Tasks")]
[assembly: AssemblyCompany("Clarius Consulting")]
[assembly: AssemblyProduct("Clarius.VisualStudio")]
[assembly: AssemblyCopyright("Copyright © Clarius Consulting 2014")]

[assembly: AssemblyVersion (ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch)]
[assembly: AssemblyInformationalVersion (
	ThisAssembly.Git.SemVer.Major + "." +
	ThisAssembly.Git.SemVer.Minor + "." +
	ThisAssembly.Git.SemVer.Patch + "-" +
	ThisAssembly.Git.Branch + "+" +
	ThisAssembly.Git.Commit)]
