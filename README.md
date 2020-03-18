# Reface.StateMachine

这是一个状态自动下推机的功能库。

依赖项
* Reface，这是一个基础库，由 .NetStandard2.0 开发

## 什么是状态自动下推机?

对某个目标事物，发生不动行为后，状态就会发生变化。
例如：

对一个文档 **保存** 后，它变成了 **草稿** 状态。
再对 **草稿** 状态的文档 **审核** 以后，会变成 **已审核** 状态，
若对 **草稿** 状态的文档 **删除** ，则会变成 **删除** 状态，
当文档已经是 **已审核** 状态时，不再允许进行 **删除** 操作。

状态机中的两个概念，一个是 **状态** 一个是 **行为**，从上面的例子来看

状态是
* 草稿
* 已审核
* 删除

行为是
* 保存
* 审核
* 删除

简单的来说，行为就是，在某个状态时，到某个状态去。

## 功能简介

使用 **Reface.StateMachine** 你可以简单的定义一个状态转移的整个过程。
当你要使用它时，只要使用 **Machine.Push(Action)** 就行了。

### 使用方法

#### 1. 引入包

使用 Nuget 搜索 Reface.StateMachine 即可，
由于 Reface.StateMachine 中有一些使用是使用 .NetStandard 开发的，所以请保证你的 .Net Framework 版本最低是 4.6.1

```shell
> Install-Package Reface.StateMachine -Version 1.0.2
```

#### 2. 定义一个 Machine

首先，你需要创建两个枚举类型，分别表示 States 和 Actions ，如：
```csharp
public enum DocumentStates
{
    Default,
    Draft,
    Checked,
    Deleted
}

public enum DocumentActions
{
    Save,
    Check,
    Delete
}
```

再使用 **CodeStateMachineBuilder<TState, Code>** 构建一个 Machine ，
**CodeStateMachineBuilder** 被设计成了使用 链式 语句声明的风格，
可以参考下面的代码。

```csharp
var builder = new CodeStateMachineBuilder<DocumentStates, DocumentActions>();
builder
    .StartWith(DocumentStates.Default) // 默认的状态
    .From(DocumentStates.Default)
        .When(DocumentActions.Save).To(DocumentStates.Draft)
    .From(DocumentState.Draft)
        .When(DocumentActions.Check).To(DocumentStates.Checked)
        .When(DocumentActions.Delete).To(DocumentStates.Deleted);

var machine = builder.Build();
machine.Push(DocumentActions.Save);
machine.Push(DocumentActions.Check);

// 此时的状态会移动到 Checked 上
```

#### 3. 监听状态的变化

我们使用 **Machine.Push(Actions)** 的主要目的是要让状态发生变化。
我们可以使用 **Machine** 上的事件来监听状态发生的变化。

事件的监听有两种方法：

**Pushed 事件**

当任意 **Action** 完成了对状态的转移后，就会触发该事件，该事件会通知此时的 Push 是执行的哪个行为，从哪个状态移动到了哪个状态。
```csharp
machine.Pushed += (sender, e) => 
{
    //sender is machine

    //e.OldState
    //e.NewState
    //e.Action
};
```

**状态 事件**

Pushed 事件面向所有的状态移动，
因此我们设计了 状态 事件和监听，可以针对某个状态监听其离开和进入。
```csharp
var listener = machine.GetListener(DocumentStates.Draft);
listener.Entered += (sender, e) =>
{
    // 刚进入这个状态时的事件
};
listener.Leaving += (sender, e) =>
{
    // 正准备离开这个状态时的事件
};
```

#### 4. 停机状态

在设计状态机时，可以指定一些停机状态。
当状态机进入停机状态后，可以发生停机事件。

设置停机状态，可以通过 [StopState] 和 Build.StopWith 两种途径设置。


---

## 通过配置文件产生 Machine 的定义

在代码设计阶段，我们往往不能一次性将复杂的状态转移写好。
虽然我们提供了 链式 定义的方法，但是面对一长段的状态转移定义，修改和维护往往比较困难。
因此，我们设计了通过配置文件，更直观的定义状态转移过程，而不需要使用链式声明法。

这是一种使用二维表格定义状态转移的方法，但是为了减少对第三方库的依赖，没有使用 Excel 格式，而使用了 Csv 文件格式。

以上述文件转移做为例子，配置文件应该像下面这个样子

| | Save | Check | Delete |
|---|---|---|---|
| Default | Draft | | |
| Draft | | Checked | Deleted |
| Deleted | | | |

**第一行第一列为空**

**第一行第一列为空**

**第一行第一列为空**

(重要的事情说三次！)

第一行是所有的行为，
第一列是所有的状态

在 行 与 列 的交接入，表示在这个状态下触发此行为时，进入的下一个状态。

**状态与行为的名称，必须与你的枚举类型中的项名称一致 ！！！**

配置文件定义好后，就可以直接利用这个 csv 文件产生 machine 了。

```csharp
var machine = CsvStateMachineBuilder<DocumentTests, DocumentActions>.FromFile(".\1.csv").Build();
```

一行代码就OK了。
当然了，还有一个小细节需要注意一下：
我们需要设置状态机的默认状态，
使用 [StartState] 特征可以在枚举类型上设定默认的状态，这个特征，只有在使用 csv 生成的状态机时才会被使用到。

```csharp
public enum DocumentTest
{
    [StartState]
    Default,
    Draft,
    Checked,
    Deleted
}
```