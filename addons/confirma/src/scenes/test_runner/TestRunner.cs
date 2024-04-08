using System;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class TestRunner : Control
{
#nullable disable
	private RichTextLabel _output;
	private ConfirmaAutoload _confirmaAutoload;
	private TestExecutor _executor;
#nullable restore

	public override void _Ready()
	{
		_output = GetNode<RichTextLabel>("%Output");
		_confirmaAutoload = GetNode<ConfirmaAutoload>("/root/Confirma");

		_executor = new(
			new Log(_confirmaAutoload.IsHeadless ? null : _output),
			new(_confirmaAutoload.IsHeadless)
		);

		_executor.ExecuteTests(Assembly.GetExecutingAssembly());
	}
}
