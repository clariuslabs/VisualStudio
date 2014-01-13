![Icon](https://raw.github.com/clariuslabs/VisualStudio/master/icon/32.png) VisualStudio Targets
============

Contains MSBuild targets that are useful when developing Visual Studio extensions. 

* Provides the following MSBuild properties for version-aware projects:
  * `VisualStudioVersion`: for VS2010, sets it to '10.0'
  * `MinimumVisualStudioVersion`: equals `VisualStudioVersion` to allow opening on any version
  * `DevEnvDir`: if it's empty, for safe command-line building. Can be overriden.
  * `PublicAssemblies`: $(DevEnvDir)\PublicAssemblies\
  * `PrivateAssemblies`: $(DevEnvDir)\PrivateAssemblies\
  * `VSSDK`: the [VSSDK install directory]\VisualStudioIntegration\Common\Assemblies\ folder. Can be overriden.
  * `VSSDK20`: $(VSSDK)v2.0\
  * `VSSDK40`: $(VSSDK)v4.0\
  * `VSToolsPath`: path to the MSBuild targets for the VSSDK

See the [readme](https://github.com/clariuslabs/VisualStudio/blob/master/nuget/Readme.txt) for the latest updates.

## Installation

To install Clarius Visual Studio Targets, run the following command in the Package Manager Console:

```
PM> Install-Package Clarius.VisualStudio
```



Icon [Infinity](http://thenounproject.com/term/infinity/9992/) by Cengiz SARI from [The Noun Project](http://thenounproject.com/).
