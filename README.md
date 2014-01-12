QQ 导出消息解析器
=================

主要用于解析 QQ 导出的消息,现在能支持解析MHT格式的导出文件.

可导出为 JSON 和 SQLite 格式的数据文件

需要注意的是,QQ导出的MHT没有谁发言这样的信息,所以导出的数据中只有 `IsBySelf`
用于判断是否是自己发言.

截图
----

![QQ 导出消息解析器](https://raw2.github.com/wenerme/QQExportMessageParser/master/screenshot.png "QQ 导出消息解析器")

导出JSON的格式
-------------

```
{
	Target:
	Messages:
	[
		{
			IsBySelf:bool
			, Date: Date
			, Content:String
			
		}
	]
}
```

导出为SQLite
-----------

表名为 MessageDB,有`IsBySelf, Content, Date`这三个值

使用的默认 Formater
-------------------

Formater 是用于格式化消息,因为MHT的消息导出中
有HTML标签,所以需要 Formater 用于规整消息格式.

默认的Formater 会去除字体样式,将Image的引用转换为
`<img src="">`, 会将引用的数据从新命名,使用md5作为文件名,
根据 ContentType 采用相应的后缀名.

Formater 能够做什么

* 格式化消息内容
* 处理 Assets 的导出目录
