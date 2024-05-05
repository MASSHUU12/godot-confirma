using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmSignalTest
{
	private static Button? _button = null;

	[SetUp]
	public static void SetUp()
	{
		_button = new();
	}

	[TearDown]
	public static void TearDown()
	{
		_button?.QueueFree();
	}

	#region ConfirmSignalExists
	[TestCase("pressed")]
	[TestCase("toggled")]
	[TestCase("button_down")]
	public static void ConfirmSignalExists_WhenSignalExists(string signalName)
	{
		_button?.ConfirmSignalExists((StringName)signalName);
	}

	[TestCase("signal_that_does_not_exist")]
	[TestCase("signal_that_is_not_here")]
	[TestCase("signal_that_is_nowhere_to_be_found")]
	public static void ConfirmSignalExists_WhenSignalDoesNotExist(string signalName)
	{
		Action action = () => _button?.ConfirmSignalExists((StringName)signalName);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
