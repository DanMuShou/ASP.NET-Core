namespace DanMu.Pan.MediatR;

/// <summary>
/// MediatR 配置辅助类，用于获取当前程序集引用
/// 该类作为定位点，供其他程序集通过 typeof(MediatRConfig).Assembly 方式获取程序集
/// 请勿删除此类，否则可能导致 MediatR 服务注册失败
/// </summary>
public class MediatRPos { }
