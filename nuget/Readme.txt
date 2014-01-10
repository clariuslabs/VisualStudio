Release Notes:

v1.0

* Provides the following MSBuild properties for version-aware projects:
	* VisualStudioVersion: for VS2010, sets it to '10.0'
	* MinimumVisualStudioVersion: equals VisualStudioVersion to allow opening on any version
	* DevEnvDir: if it's empty, for safe command-line building. Can be overriden.
	* PublicAssemblies: $(DevEnvDir)\PublicAssemblies\
	* PrivateAssemblies: $(DevEnvDir)\PrivateAssemblies\
	* VSSDK: the [VSSDK install directory]\VisualStudioIntegration\Common\Assemblies\ folder. Can be overriden.
	* VSSDK20: $(VSSDK)v2.0\
	* VSSDK40: $(VSSDK)v4.0\
	* VSToolsPath: path to the MSBuild targets for the VSSDK