namespace WiseByteExtensions.Core.Validators;

public static class DirectoryValidator
{
    /// <summary>
    /// Returns true if the directory string is syntactically valid
    /// NOT if it exists or is writable.
    /// </summary>
    public static bool IsValidDirectoryString(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        // 1. Invalid path characters (incomplete but still needed)
        if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            return false;

        // 2. Additional Windows-invalid characters
        char[] invalidDirChars = { '<', '>', '"', '|', '?', '*' };

        if (path.IndexOfAny(invalidDirChars) >= 0)
            return false;

        // 3. Colon allowed only in drive letter
        // Valid: "C:\Folder"
        // Invalid: "C:\Fol:der"
        int colonCount = path.Count(c => c == ':');
        if (colonCount > 1)
            return false;

        if (colonCount == 1)
        {
            // Must be in position 1: "C:"
            if (path[1] != ':')
                return false;
        }

        // 4. Should contain a separator or drive prefix
        if (!path.Contains('\\') && !path.Contains('/') && !path.Contains(':'))
            return false;

        return true;
    }



    /// <summary>
    /// Checks if the given directory path exists on disk.
    /// </summary>
    public static bool DirectoryExists(string? path)
    {
        if (!IsValidDirectoryString(path))
            return false;

        return Directory.Exists(path!);
    }


    /// <summary>
    /// Checks if directory is writable. 
    /// Creates a temporary file — safe and reliable.
    /// </summary>
    public static bool IsDirectoryWritable(string path)
    {
        try
        {
            if (!DirectoryExists(path))
                return false;

            string testFile = Path.Combine(path, $"write_test_{Guid.NewGuid()}.tmp");

            // Try creating a file
            File.WriteAllText(testFile, "TEST");

            // Cleanup
            File.Delete(testFile);

            return true;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Checks if directory is creatable (including parent folder checks).
    /// </summary>
    public static bool CanCreateDirectory(string path)
    {
        try
        {
            if (DirectoryExists(path))
                return IsDirectoryWritable(path);

            var parent = Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(parent))
                return false;

            // parent must exist and be writable
            if (!DirectoryExists(parent))
                return false;

            return IsDirectoryWritable(parent);
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Full validation: no invalid characters, not in restricted folders,
    /// writable or creatable.
    /// </summary>
    public static bool IsUsableDirectory(string? path)
    {
        if (!IsValidDirectoryString(path))
            return false;

        // Avoid system-restricted directories
        if (IsRestrictedSystemPath(path!))
            return false;

        // Directory exists → must be writable
        if (DirectoryExists(path))
            return IsDirectoryWritable(path!);

        // Directory does not exist → must be creatable
        return CanCreateDirectory(path!);
    }


    /// <summary>
    /// Detects system folders that should not be used for business data
    /// </summary>
    public static bool IsRestrictedSystemPath(string path)
    {
        string lower = path.ToLowerInvariant();

        return lower.Contains(@"\windows") ||
               lower.Contains(@"\program files") ||
               lower.Contains(@"\programdata") ||
               lower.Contains(@"\users\public") ||
               lower.Contains(@"\system32");
    }


    /// <summary>
    /// Validate if a file path is safe for SQLite database creation.
    /// </summary>
    public static bool IsValidDatabaseFilePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        if (Path.GetExtension(path).ToLower() != ".db")
            return false;

        var dir = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(dir))
            return false;

        return IsUsableDirectory(dir);
    }
}
