# <center>简单，易扩展的泛型状态机与消息中心</center>

## 1 快速开始
### 1.1 消息中心的使用
#### 1.1.1 基本的事件注册发送
```c#
//声明一个消息中心，消息中心的事件key的类行是string，传入构造函数参数为储存消息的容器
var messenger = new Messenger<string>(new Dictionary<string, Delegate>());
//监听一个事件
string s;
messenger.AddListener("event1", () => { s = "Hello World"});

//触发事件
messenger.Broadcast("event1");
//此时字符串将被赋值“Hello World”
```
#### 1.1.2 带参数的事件注册发送
```c#
//声明一个消息中心，消息类行是string，传入参数为储存消息的容器
var messenger = new Messenger<string>(new Dictionary<string, Delegate>());
//监听一个带参数的事件
string s;
messenger.AddListener<string>("event2", (string value) => { value = "Hello World"; });
//触发事件，传入参数
messenger.Broadcast<string>("event2", s);
////此时字符串将被赋值“Hello World”
```
#### 1.1.3 带多个参数，带返回类型的注册发送事件的方式
与带参数事件注册与发送类似，在此不做详细介绍
#### 1.1.4 如何扩展？
消息中心注册需要泛型的委托，触发事件的广播也需传入多个参数，<br>
如果需要的特定的委托类型怎么办？您可以找到Router项目观察如下目录，<br>
+ Messenger0 
+ Messenger1
+ Messenger2
+ Messenger3
<br>它们分别为没有参数，一个参数，两个参数，三个参数的Messenger类，<br>
按照文件里的代码,非常容易扩展出其他参数个数的Messenger类。

### 1.2 有限状态机
### 1.2.1 声明一个状态类
```c#
//第一个泛型string参数：需要继承StateBase类，第一个传入泛型参数为状态名字的类型，
//第二个泛型string参数：消息中心事件key的类型，StateBase继承Messenger
// IStart接口： 转换到当前状态时调用，可以不添加这个接口
// IOver接口：结束当前状态时调用，可以不添加这个接口
public class TestState : StateBase<string, string> ,IStart,IOver
    {
        public int starttimes = 0;
        public int overtimes = 0;
        public TestState()
        {

        }
        public TestState(string id):base(id)
        {

        }
        public void Over()
        {
            overtimes += 1;
        }

        public void Start()
        {
            starttimes += 1;
        }
    }
```
### 1.2.2 声明一个有限状态机
```c#
//第一个泛型string参数：State类名字的类行
//第二个泛型string参数：StateBase继承Messenger类，事件key的类行
   var fsm = new FiniteStateMachine<string, string>();
```
### 1.2.3添加并转换状态
```c#
  var state = new TestState("statename");
  //将状态添加到状态机中
  fsm.AddState(state);
  //根据状态名字“statename",转换到该状态;
  fsm.Translation("statename");
  //继承了IStat接口的TestState的Start函数将被调用
  
```


