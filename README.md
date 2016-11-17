# ![Icon](https://raw.github.com/clariuslabs/VisualStudio/master/icon/32.png) VisualStudio Targets

[![Build status](https://ci.appveyor.com/api/projects/status/upvkqk92al5vn0an?svg=true)](https://ci.appveyor.com/project/MobileEssentials/visualstudio) 
[![Latest version](https://img.shields.io/nuget/v/Clarius.VisualStudio.svg)](https://www.nuget.org/packages/Clarius.VisualStudio)
[![License](https://img.shields.io/github/license/clariuslabs/VisualStudio.svg)](http://www.apache.org/licenses/LICENSE-2.0)

Contains MSBuild targets that are useful when developing Visual Studio extensions. 

* Provides the following MSBuild properties for version-aware projects:
  * `VisualStudioVersion`: ensures it's always set (i.e. under dev15+, sets it to '15.0' on command line builds
  * `MinimumVisualStudioVersion`: equals `VisualStudioVersion` to allow opening on any version
  * `DevEnvDir`: if it's empty, for safe command-line building. Can be overriden.
  * `PublicAssemblies`: $(DevEnvDir)\PublicAssemblies\
  * `PrivateAssemblies`: $(DevEnvDir)\PrivateAssemblies\
  * `VSSDK`: the [VSSDK install directory]\VisualStudioIntegration\Common\Assemblies\ folder. Can be overriden.
  * `VSSDK20`: $(VSSDK)v2.0\
  * `VSSDK40`: $(VSSDK)v4.0\
  * `VSToolsPath`: path to the MSBuild targets for the VSSDK
  * `Dev`: simple version number from Visual Studio version, without the decimal (i.e. '15')
  * `DEV[n]` defined constant for conditional code compilation (i.e. #if DEV15, to compile 
    only when building for VS2017). Gets `n` from `$(Dev)` value.


* Smarter and simpler template authoring. Just set BuildAction to None on all your 
  template content as well as the .vstemplate, and they become Smart Templates automatically:
	* Supports <Include> metadata to add shared artifacts to the generated ZIP files
	* Does not regenerate ZIP files if content didn't change
	* Supports linked files that are copied to the output directory


More in-depth tutorials on using this project is available in the [wiki](https://github.com/clariuslabs/VisualStudio/wiki).

See the [readme](https://github.com/clariuslabs/VisualStudio/blob/master/nuget/Readme.txt) for the latest change log.

## Installation

To install Clarius Visual Studio Targets, run the following command in the Package Manager Console:

```
PM> Install-Package Clarius.VisualStudio
```



Icon [Infinity](http://thenounproject.com/term/infinity/9992/) by Cengiz SARI from [The Noun Project](http://thenounproject.com/).




[![Build Status](https://www.myget.org/BuildSource/Badge/clarius?identifier=97a997db-cde0-4f12-9d5f-a7e86b682873 "Build Status")](https://www.myget.org/gallery/clarius)
====================
This project is sponsored by Clarius Labs

[![Clarius Labs][2]][1]


  [1]: http://clariuslabs.github.io/
  [2]: http://clariuslabs.github.io/media/clariuslabs.png (Clarius Labs Logo)
