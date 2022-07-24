# tpantlr2
Examples of the book "The Definitive ANTLR 4 Reference" by Terence Parr translated in C#

Thanks to Ken Domino, who is maintaining Antlr4BuildTasks to explain how to generate code in Visual Studio.

People should use either Antlr4BuildTasks

<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>
	<ItemGroup>
		<Content Include="input">
			<CopyToOutputDirectory>Always</CopyToOutputDirectory>
		</Content>
	</ItemGroup>
	<ItemGroup>
		<PackageReference Include="Antlr4.Runtime.Standard" Version="4.10.1" />
		<PackageReference Include="Antlr4BuildTasks" Version="10.7" />
	</ItemGroup>
	<ItemGroup>
		<Antlr4 Include="Hello.g4" />
	</ItemGroup>
</Project>

Or, add in rules to call the Antlr4 tool in their .csproj files:

<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>
	<ItemGroup>
		<Content Include="input">
			<CopyToOutputDirectory>Always</CopyToOutputDirectory>
		</Content>
	</ItemGroup>
	<ItemGroup>
		<PackageReference Include="Antlr4.Runtime.Standard" Version="4.10.1" />
	</ItemGroup>
	<Target Name="tool">
		<Exec Command="java -jar ../antlr4-4.10.1-complete.jar -Dlanguage=CSharp *.g4"/>
	</Target>
	<PropertyGroup>
		<BuildDependsOn>
			tool;
			$(BuildDependsOn)
		</BuildDependsOn>
	</PropertyGroup>
	<PropertyGroup>
		<CoreCompileDependsOn>
			tool;
			$(CoreCompileDependsOn)
		</CoreCompileDependsOn>
	</PropertyGroup>
	<ItemGroup>
		<Compile Include="Program.cs;HelloBaseListener.cs;HelloLexer.cs;HelloListener.cs;HelloParser.cs"/>
		<CompileDesignTime Include="Program.cs;HelloBaseListener.cs;HelloLexer.cs;HelloListener.cs;HelloParser.cs"/>
	</ItemGroup>
	<PropertyGroup>
		<EnableDefaultCompileItems>false</EnableDefaultCompileItems>
	</PropertyGroup>
	<PropertyGroup>
		<NoWarn>3021;1701;1702</NoWarn>
	</PropertyGroup>
</Project>

