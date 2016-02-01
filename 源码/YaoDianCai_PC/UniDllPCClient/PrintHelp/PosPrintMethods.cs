using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UniDllPCClient.PrintHelp
{
    public class PosPrintMethods
    {
        /// <summary>
        /// 链接打印机
        /// </summary>
        /// <param name="lpName"></param>
        /// <param name="nComBaudrate"></param>
        /// <param name="nComDataBits"></param>
        /// <param name="nComStopBits"></param>
        /// <param name="nComParity"></param>
        /// <param name="nParam"></param>
        /// <returns>
        /// 如果函数调用成功，返回一个已打开的端口句柄。
        /// 如果函数调用失败，返回值为 INVALID_HANDLE_VALUE （-1）。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern IntPtr POS_Open(string lpName, int nComBaudrate, int nComDataBits, int nComStopBits, int nComParity, int nParam);
        /// <summary>
        /// 设置打印机的打印模式。
        /// </summary>
        /// <param name="nPrintMode">
        /// 指定打印模式
        /// POS_PRINT_MODE_STANDARD         0x00 标准模式（行模式） 
        /// POS_PRINT_MODE_PAGE             0x01 页模式 
        /// POS_PRINT_MODE_BLACK_MARK_LABEL 0x02 黑标记标签模式 
        /// POS_PRINT_MODE_WHITE_MARK_LABEL 0x03 白标记标签模式 
        /// </param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_SetMode(int nPrintMode);
        /// <summary>
        /// 设置字符的行高。
        /// </summary>
        /// <param name="nDistance">指定行高点数。可以为 0 到 255。每点的距离与打印头分辨率相关。</param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_SetLineSpacing(int nDistance);
        /// <summary>
        /// 设置字符的右间距（相邻两个字符的间隙距离）。
        /// </summary>
        /// <param name="nDistance">指定右间距的点数。可以为 0 到 255。每点的距离与打印头分辨率相关。</param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_SetRightSpacing(int nDistance);
        /// <summary>
        /// 把将要打印的字符串数据发送到打印缓冲区中，并指定X 方向（水平）上的绝对起始点位置，指定每个字符宽度和高度方向上的放大倍数、类型和风格。
        /// </summary>
        /// <param name="pszString">指向以 null 结尾的字符串缓冲区。</param>
        /// <param name="nOrgx">指定 X 方向（水平）的起始点位置离左边界的点数。可以为 0 到 65535。</param>
        /// <param name="nWidthTimes">指定字符的宽度方向上的放大倍数。可以为 1到 6。</param>
        /// <param name="nHeightTimes">指定字符高度方向上的放大倍数。可以为 1 到 6。</param>
        /// <param name="nFontType">
        /// 指定字符的字体类型。
        /// POS_FONT_TYPE_STANDARD      0x00 标准 ASCII 
        /// POS_FONT_TYPE_COMPRESSED    0x01 压缩 ASCII  
        /// POS_FONT_TYPE_UDC           0x02 用户自定义字符 
        /// POS_FONT_TYPE_CHINESE       0x03 标准 “宋体” 
        /// </param>
        /// <param name="nFontStyle">
        /// 指定字符的字体风格。
        /// POS_FONT_STYLE_NORMAL           0x00 正常 
        /// POS_FONT_STYLE_BOLD             0x08 加粗 
        /// POS_FONT_STYLE_THIN_UNDERLINE   0x80 1点粗的下划线 
        /// POS_FONT_STYLE_THICK_UNDERLINE  0x100 2点粗的下划线 
        /// POS_FONT_STYLE_REVERSE          0x400 反显（黑底白字） 
        /// POS_FONT_STYLE_SMOOTH           0x800 平滑处理（用于放大时） 
        /// </param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_S_TextOut(string pszString, int nOrgx, int nWidthTimes, int nHeightTimes, int nFontType, int nFontStyle);
        /// <summary>
        /// 向前走纸
        /// </summary>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：/POS_FAIL/POS_ERROR_INVALID_HANDLE。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_FeedLine();
        /// <summary>
        /// 切纸。
        /// </summary>
        /// <param name="nMode">指定切纸模式。
        /// POS_CUT_MODE_FULL       0x00 全切 
        /// POS_CUT_MODE_PARTIAL    0x01 半切 
        /// </param>
        /// <param name="nDistance">指定进纸长度的点数。可以为 0 到 255。每点的距离与打印头分辨率相关。</param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER。
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_CutPaper(int nMode, int nDistance);
        /// <summary>
        /// 设置打印机的移动单位。
        /// </summary>
        /// <param name="nHorizontalMU">把水平方向上的移动单位设置为 25.4 / nHorizontalMU 毫米。可以为0到255。</param>
        /// <param name="nVerticalMU">把垂直方向上的移动单位设置为 25.4 / nVerticalMU 毫米。可以为0到255。</param>
        /// <returns>
        /// 如果函数成功，则返回值为 POS_SUCCESS。
        /// 如果函数失败，则返回值为以下值之一：POS_FAIL/POS_ERROR_INVALID_HANDLE/POS_ERROR_INVALID_PARAMETER
        /// </returns>
        [DllImport("POSDLL.DLL")]
        public static extern int POS_SetMotionUnit(int nHorizontalMU, int nVerticalMU);
        /// <summary>
        /// 开始把发往打印机（端口）的数据保存到指定的文件。
        /// </summary>
        /// <param name="lpFileName">保存数据的文件名称，是null结尾的字符串。可以是绝对路径，也可以是相对路径。</param>
        /// <param name="bToPrinter">
        /// TRUE ： 指定是否在保存数据到文件的同时，把数据也发送到打印机（端口）。 
        /// FALSE ：指定是否在保存数据到文件的同时，不把数据也发送到打印机（端口）。
        /// </param>
        [DllImport("POSDLL.DLL")]
        public static extern void POS_BeginSaveFile(string lpFileName, bool bToPrinter);
        /// <summary>
        /// 获取dll版本信息
        /// </summary>
        /// <param name="pnMajor"></param>
        /// <param name="pnMinor"></param>
        /// <returns></returns>
        [DllImport("POSDLL.dll")]
        public extern static int POS_GetVersionInfo(out IntPtr pnMajor, out IntPtr pnMinor);
    }
}
