using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

#if PRODUCTION
    [assembly: AssemblyTitle("EasiWorx")]
    [assembly: AssemblyProduct("EasiWorx")]
    [assembly: AssemblyVersion("1.0.86")]
    [assembly: AssemblyFileVersion("1.0.86")]
#elif PREVIEW
    [assembly: AssemblyTitle("EasiWorx Preview")]  
    [assembly: AssemblyProduct("EasiWorx Preview")]
    [assembly: AssemblyVersion("1.0.86")] 
    [assembly: AssemblyFileVersion("1.0.86")] 
#elif STAGING
    [assembly: AssemblyTitle("EasiWorx Staging")]  
    [assembly: AssemblyProduct("EasiWorx Staging")]
    [assembly: AssemblyVersion("1.0.86")] 
    [assembly: AssemblyFileVersion("1.0.86")] 
#elif MOJAFF
    [assembly: AssemblyTitle("EasiWorx Mojaff Preview")]
    [assembly: AssemblyProduct("EasiWorx Mojaff")]
    [assembly: AssemblyVersion("1.0.86")]
    [assembly: AssemblyFileVersion("1.0.86")]
#else
[assembly: AssemblyTitle("EasiWorx Dev")]
    [assembly: AssemblyProduct("EasiWorx Dev")]
    [assembly: AssemblyVersion("1.0.114")]
    [assembly: AssemblyFileVersion("1.0.114")]
#endif

[assembly: AssemblyDescription("Financial Services Software")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("EasiWorx (Pty) Ltd")]
[assembly: AssemblyCopyright("Copyright© EasiWorx 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
//[assembly: Guid("fa30a407-6e11-4df4-b976-7bb87f661f05")]
[assembly: Guid("d2c874e6-d767-4cad-a5d2-3f190dd72801")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
//automaticall set the assembly version - will break compatability with uncompiled dependencies
//[assembly: AssemblyVersion("1.0.0.0")]

//TO DO : Update the assembly file version to force a database schema update.
// commenting this out this will set the fileversion same as assembly version

//logging
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

