namespace DanMu.Pan.Helper;

// OK

/// <summary>
/// 提供目录大小计算功能的工具类
/// </summary>
public static class DirectorySizeCalculation
{
    /// <summary>
    /// 计算指定目录的总大小
    /// </summary>
    /// <param name="dInfo">要计算大小的目录信息对象</param>
    /// <param name="includeSubDir">是否包含子目录的大小计算，true表示递归计算所有子目录，false表示只计算当前目录</param>
    /// <returns>目录的总大小（以字节为单位）</returns>
    public static long DirectorySize(DirectoryInfo dInfo, bool includeSubDir)
    {
        // 计算当前目录下所有文件的大小总和
        var total = dInfo.EnumerateFiles().Sum(fileInfo => fileInfo.Length);
        if (includeSubDir)
        {
            // 递归计算所有子目录的大小并累加
            total += dInfo.EnumerateDirectories().Sum(dirInfo => DirectorySize(dirInfo, true));
        }
        return total;
    }
}
