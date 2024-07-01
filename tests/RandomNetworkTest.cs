using System;
using System.Linq;
using System.Net.Sockets;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomNetworkTest
{
	private readonly static Random _rg = new();

	#region NextIPAddress
	[Repeat(5)]
	[TestCase]
	public static void NextIPAddress_GeneratesValidIPv4Address()
	{
		var ipAddress = _rg.NextIPAddress();

		Confirm.IsTrue(ipAddress.AddressFamily == AddressFamily.InterNetwork);
		Confirm.IsTrue(ipAddress.GetAddressBytes().Length == 4);
	}

	[Repeat(5)]
	[TestCase]
	public static void NextIPAddress_FirstOctetIsAlwaysOdd()
	{
		var ipAddress = _rg.NextIPAddress();

		Confirm.IsTrue((ipAddress.GetAddressBytes()[0] & 1) == 1);
	}
	#endregion

	#region NextIP6Address
	[Repeat(5)]
	[TestCase]
	public static void NextIP6Address_GeneratesValidIPv6Address()
	{
		var ipAddress = _rg.NextIP6Address();

		Confirm.IsTrue(ipAddress.AddressFamily == AddressFamily.InterNetworkV6);
		Confirm.IsTrue(ipAddress.GetAddressBytes().Length == 16);
	}

	[Repeat(5)]
	[TestCase]
	public static void NextIP6Address_FirstHextetIsAlwaysOdd()
	{
		var ipAddress = _rg.NextIP6Address();

		Confirm.IsTrue((ipAddress.GetAddressBytes()[0] & 1) == 1);
	}
	#endregion

	#region NextEmail
	[TestCase]
	public static void NextEmail_InvalidLengthParameters()
	{
		Confirm.Throws<ArgumentException>(() => _rg.NextEmail(-1, 12));
		Confirm.Throws<ArgumentException>(() => _rg.NextEmail(12, 8));
	}

	[Repeat(5)]
	[TestCase]
	public static void NextEmail_ReturnsValidEmail()
	{
		var minLength = _rg.Next(1, 64);
		var maxLength = _rg.Next(minLength, minLength + 64);
		var email = _rg.NextEmail(minLength, maxLength);

		email.ConfirmNotNull();
		email.Contains('@').ConfirmTrue();

		var parts = email.Split('@');
		Confirm.IsTrue(parts[0].Length >= minLength && parts[0].Length <= maxLength);
	}

	[Repeat(5)]
	[TestCase]
	public static void NextEmail_LocalPartContainsValidCharacters()
	{
		var email = _rg.NextEmail();

		var localPart = email.Split('@')[0];
		Confirm.IsTrue(localPart.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'));
	}
	#endregion
}
