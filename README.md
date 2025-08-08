在.Net中集成Apollo

### 说明事项

项目里实现了俩种配置模式

1.k8s模式

包含以下文件

appsettings.Development.json	--开发环境

appsettings.json	--测试环境、灰度环境、生产环境

利用运行环境变量(ASPNETCORE_ENVIRONMENT)来区分开发与线上环境

其中线上环境的配置利用k8s的特性,每个Namespace为一个环境,并配置每个命名空间的访问Apollo的服务别名(k8s-apollo-service)

2.运行环境自定义变量模式

包含以下文件

appsettings.dev.json	--开发环境

appsettings.fat.json		--测试环境

appsettings.pro.json	--生产环境

启动编译好程序时在运行环境里添加自定义变量(RUN_ENV)来区分环境

启动示例：dotnet Apollo.dll -RUN_ENV=fat
