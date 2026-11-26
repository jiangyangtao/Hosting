# FrontendApi

前端元数据项目，用于API接口输入/输出的基本描述，采用 RESTful API 风格

## 数据结构

### 基本结构

```
Module 模块
    └─ TableComponent 表格组件
       |
       ├─ AddForm 内置添加表单
       |
       ├─ EditForm 内置编辑表单
       |
       ├─ QueryForm 内置查询表单
       |
       ├─ Fields 字段集
       |
    ├─ FormComponent 表单组件
    |
    ├─ QueryFormComponent 查询表单组件
```

- 通过 Module 和 ComponentName 两级唯一性来定位组件
- 组件禁止重复注册
- AddFrom 与 EditForm 禁止使用同一个类
- 组件对应的类通用时，可通过继承实现多态
- 多态继承时，父类与派生类的类级特性互相不影响
- TableComponent 内置的 AddForm/EditForm/QueryForm 同时会向 Module 注册为独立组件
- TableComponent 内置的组件禁止再进行注册
- 使用的项目必须在工程文件中设置 `<GenerateDocumentationFile>true</GenerateDocumentationFile>`

### TableComponent 组件

基本结构

```
- TableComponent 组件
    └─ AddForm 内置添加表单
        └─ Controls 控件集
    ├─ EditForm 内置编辑表单
        └─ Controls 控件集
    ├─ QueryForm 内置查询表单
        └─ Controls 控件集
    ├─ Fields 字段集
    ├─ DataStatus 数据状态操作
    ├─ DeleteAction 删除操作
    ├─ BatchDeleteAction 批量删除操作
    ├─ ImportAction 导入操作
    ├─ ExportAction 导出操作
```

- 用于表格的基本描述和操作
- 通过 TableComponentAttribute 进行配置添加表单、编辑表单、查询表单
- 必须配置 ModuleAttribute 用于描述 Module，详见 ModuleAttribute
- 必须配置 HttpActionAttribute 用于描述接口，详见 HttpActionAttribute
- AddForm 和 EditForm 为内置组件时，无须指定 Module，但必须配置 HttpActionAttribute，用于描述接口
- DataStatus 为独立的数据状态操作，通过在 DataStatusAttribute 指定在字段上，一个表格组件只能指定一个
- DeleteAction 默认为 Delete 行为，一个表格组件只能指定一个
- BatchDeleteAction 默认为 Delete 行为，一个表格组件只能指定一个
- ImportAction 默认为 Post 行为，一个表格组件只能指定一个
- ExportAction 默认为 Get 行为，一个表格组件只能指定一个

### FormComponent 组件

基本结构

```
- FormComponent 组件
    └─ Controls 控件集
```

- 用于添加表单或者编辑表单的基本描述和操作
- 独立注册时必须配置 FormAttribute 用于描述接口，详见 HttpActionAttribute
- 独立注册时必须配置 ModuleAttribute 用于描述 Module，详见 ModuleAttribute
- 内置为表格时无须指定 Module，但必须配置 HttpActionAttribute，用于描述接口
- DisplayMode 的 Modal 为弹窗，Drawer 为抽屉，默认为 Drawer 抽屉

### QueryFormComponent 组件

基本结构

```
- QueryFormComponent 组件
    └─ Controls 控件集
```

- 用于查询表单的基本描述
- 无须配置 HttpActionAttribute
- 独立注册时配置 QueryFormAttribute 用于描述 Module，详见 ModuleAttribute
- 内置为表格时无须配置 QueryFormAttribute

### DataStatus 数据状态
- 只支持 Enum 和 bool 类型
- 固定为开关，无法更改
- 

#### Field 字段

| 属性      | 名称       | 描述                                    | 对应 Attribute  |
| --------- | ---------- | --------------------------------------- | --------------- |
| Name      | 字段名     |                                         |                 |
| Text      | 名称       | 获取注释，不写注释则为空                |                 |
| Format    | 格式化     | 对应字段类型的格式化，用于前端展示      | `[Formatter]`） |
| FieldType | 字段类型   | 字段基本类型，详见 [FieldType 字段类型] |                 |
| IsKey     | 是否为主键 | 必须为唯一                              | `[UniqueKey]`   |

### FieldType 字段类型

| 属性类型                                       | FieldType |
| ---------------------------------------------- | --------- |
| short / int / long                             | Integer   |
| float / double / decimal                       | Decimal   |
| DateTime / DateTimeOffset                      | DateTime  |
| bool                                           | Boolean   |
| enum                                           | Enum      |
| `T[]`、`List<T>`、`IList<T>`、`IEnumerable<T>` | Set       |
| [Tags]                                         | Tags      |
| string                                         | String    |

- 默认为 `string`，未支持的类型则默认为 `string`
- Set 集合只支持 `Arrry<T>`、`List<T>`、`IList<T>`、`IEnumerable<T>`

### Control 控件

基本属性
| 属性 | 名称 | 描述 | 对应 Attribute |
| --------- | ---------- | --------------------------------------- | --------------- |
| Name | 字段名 | | |
| Text | 名称 | 获取注释，不写注释则为空 | |
| Format | 格式化 | 对应字段类型的格式化，用于前端展示 | `[Formatter]`） |
| Required | 是否必须 | true 表单组件上会出现必填的红色星号 \*，且会校验非空 ||
| DefaultValue | 默认值 |默认值无类型校验，设置的时候请对应到属性的类型，否则前端控件无法识别| `DefaultValueAttribute` |
| ControlType | 控件类型 | ||

- 默认为 `string`，未支持的类型则默认为 `string`
- 集合只支持 `Arrry<T>`、`List<T>`、`IList<T>`、`IEnumerable<T>`

#### 文本框 `[Input]`

```csharp
/// <summary>
/// 名称
/// </summary>
[Input]
[MaxLength(64)]
public string? Name { get; set; }
```

- String 类型默认为 Input 控件
- 要设置 Input 控件的属性，使用 `InputAttribute` 特性

### 数字框 `[InputNumber]`

```csharp
/// <summary>
/// 数量
/// </summary>
[InputNumber]
[Range(0, 100)]
public int Count { get; set; }
```

- 用于无小数类型的数字输入框
- short / int / long 默认为 InputNumber
- 通过 `RangeAttribute` 设置最小值、最大值
- 要设置 InputNumber 控件的属性，使用 `InputNumberAttribute` 特性
- Step 属性设置每次改变步数，默认为 1

### 小数框 `[InputDecimal]`

```csharp
/// <summary>
/// 价格
/// </summary>
[InputDecimal]
[Range(0, 100)]
public decimal Price { get; set; }
```

- 用于小数类型的数字输入框
- float / double / decimal 默认为 InputDecimal
- 通过 `RangeAttribute` 设置最小值、最大值
- 要设置 InputDecimal 控件的属性，使用 `InputDecimalAttribute` 特性
- Step 属性设置每次改变步数，默认为 0.1

### 其它特性

#### ModuleAttribute

- 构造函数的 moduleType 参数会获取 type.Name 赋值到 Module， 获取注释赋值到 ModuleName，未写注释则为空

#### HttpActionAttribute

- 构造函数的 controllerType 必须继承自 Microsoft.AspNetCore.Mvc.ControllerBase
- controllerType 会获取 method 的行为特性，自动判断 HttpMethodType
- ApiVersion 默认为 v1
- ServiceName 不配置则默认为当前使用项目的 DefaultServiceName

#### SelectAttribute

- 用于 Enum / bool 或远程字典类型字段的下拉控件
- SourceType 为 Enum 只支持字段类型为 Enum 或者 bool
- SourceType 为 API 则无限制
- SourceType 为 Enum 且字段类型也为 Enum 时，会反射获取 Enum 值，再获取注释用于 Text
- Text 支持 DisplayAttribute 特性
- 注释不支持 `<see>` / `<para>` 标签
- SourceType 为 Enum 且字段类型为 bool 时，可设置 TrueText("是"描述) / FalseText("否"描述) ，TrueText 默认为"是"，FalseText 默认为"否"
- SourceType 为 API 时，必须配置 http action

#### UploadAttribute

- 用于有字段的表单文件上传
- 配置 http action，上传文件会默认请求远程地址
- 不配置 http action，文件会以 JavaScript 的 FormData 上传到对应表单的接口

#### FormUploadAttribute

- 用于无字段的表单文件上传，可重复使用
- 配置 http action，上传文件会默认请求远程地址
- 不配置 http action，文件会以 JavaScript 的 FormData 上传到对应表单的接口
