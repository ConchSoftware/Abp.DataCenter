# DataCenter

### Design UML
![1661845304603](https://user-images.githubusercontent.com/37917403/187379112-c0e54f3c-0e51-45eb-8634-e4b9c45e3a6b.png)

# Add Export Config
var configdata = new ExcelExportConfigMaster(GuidGenerator.Create(), "员工信息导出", "员工信息", true, ExcelExportConfigTypeEnum.xlsx);
configdata.AddItems("name", "员工姓名", ExcelColumnType.String, 120, 1);
configdata.AddItems("age", "员工年龄", ExcelColumnType.String, 60, 2);
configdata.AddItems("area", "个人爱好", ExcelColumnType.String, 200, 3);
await _excelExportConfigRepository.InsertAsync(configdata);
