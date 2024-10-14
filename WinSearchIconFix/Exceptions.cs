using System;

public class AppIdNullException : Exception
{
    public AppIdNullException()
        : base("App ID was unavailable in XML File") { }

    public AppIdNullException(string message)
        : base(message) { }

    public AppIdNullException(string message, Exception inner)
        : base(message, inner) { }
}

public class AppxManifestXmlNotFound : Exception
{
    public AppxManifestXmlNotFound()
        : base("App Manifest XML was nout found") { }

    public AppxManifestXmlNotFound(string message)
        : base(message) { }

    public AppxManifestXmlNotFound(string message, Exception inner)
        : base(message, inner) { }
}

public class InstallLocationUnavailable : Exception
{
    public InstallLocationUnavailable()
        : base("Install Location is Unavailable.") { }

    public InstallLocationUnavailable(string message)
        : base(message) { }

    public InstallLocationUnavailable(string message, Exception inner)
        : base(message, inner) { }
}

public class LogoLocationUnavailable : Exception
{
    public LogoLocationUnavailable()
        : base("Logo was Unavailable.") { }

    public LogoLocationUnavailable(string message)
        : base(message) { }

    public LogoLocationUnavailable(string message, Exception inner)
        : base(message, inner) { }
}
