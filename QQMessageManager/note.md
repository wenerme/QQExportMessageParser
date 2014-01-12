
匹配 MIME 的Regex
-----------------

### 匹配出 header 部分

使用 `indexOf("\r\n\r\n")` 来获取头长度.

### 匹配头的键值对

flag: `/mge`

```regex
^
(?<key>[^:]+) # 匹配键
:
(?<value> # 值有两种
(?:
[^\n\r] # 单行值
|\r\n\t
)+) # 多行值
```

### Content-Type 参数解析

flag: `/e`

```regex
(?<content_type>[^;]+);
(?<parameters>
(	
	(?<key>[^=]+)=
	(?<value>[^;]+)
	;?
)*
)
```

一些注记
--------

每条消息和每条日期为一行,每行为一个 TR

消息对象在第二个TR
