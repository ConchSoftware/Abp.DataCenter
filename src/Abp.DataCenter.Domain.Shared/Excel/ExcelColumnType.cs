using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DataCenter.Excel
{

    /// <summary>
    /// 导出列类型
    /// </summary>
    public enum ExcelColumnType
    {
        String = 0,
        Datetime = 1,
        Int = 2,
        Double = 3,
        Money = 4
    }
}
