using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RandomNetworkTest
{
    private static readonly Random _rg = new();

    #region NextIPAddress
    [Repeat(5)]
    [TestCase]
    public void NextIPAddress_GeneratesValidIPv4Address()
    {
        IPAddress ipAddress = _rg.NextIPAddress();

        _ = Confirm.IsTrue(ipAddress.AddressFamily == AddressFamily.InterNetwork);
        _ = Confirm.IsTrue(ipAddress.GetAddressBytes().Length == 4);
    }

    [Repeat(5)]
    [TestCase]
    public void NextIPAddress_FirstOctetIsAlwaysOdd()
    {
        IPAddress ipAddress = _rg.NextIPAddress();

        _ = Confirm.IsTrue((ipAddress.GetAddressBytes()[0] & 1) == 1);
    }
    #endregion NextIPAddress

    #region NextIP6Address
    [Repeat(5)]
    [TestCase]
    public void NextIP6Address_GeneratesValidIPv6Address()
    {
        IPAddress ipAddress = _rg.NextIP6Address();

        _ = Confirm.IsTrue(ipAddress.AddressFamily == AddressFamily.InterNetworkV6);
        _ = Confirm.IsTrue(ipAddress.GetAddressBytes().Length == 16);
    }

    [Repeat(5)]
    [TestCase]
    public void NextIP6Address_FirstHextetIsAlwaysOdd()
    {
        IPAddress ipAddress = _rg.NextIP6Address();

        _ = Confirm.IsTrue((ipAddress.GetAddressBytes()[0] & 1) == 1);
    }
    #endregion NextIP6Address

    #region NextEmail
    [TestCase]
    public void NextEmail_InvalidLengthParameters()
    {
        _ = Confirm.Throws<ArgumentException>(static () => _rg.NextEmail(-1, 12));
        _ = Confirm.Throws<ArgumentException>(static () => _rg.NextEmail(12, 8));
    }

    [Repeat(5)]
    [TestCase]
    public void NextEmail_ReturnsValidEmail()
    {
        int minLength = _rg.Next(1, 64);
        int maxLength = _rg.Next(minLength, minLength + 64);
        string email = _rg.NextEmail(minLength, maxLength);

        _ = email.ConfirmNotNull();
        _ = email.Contains('@').ConfirmTrue();

        string[] parts = email.Split('@');
        _ = Confirm.IsTrue(parts[0].Length >= minLength && parts[0].Length <= maxLength);
    }

    [Repeat(5)]
    [TestCase]
    public void NextEmail_LocalPartContainsValidCharacters()
    {
        string email = _rg.NextEmail();

        string localPart = email.Split('@')[0];
        _ = Confirm.IsTrue(localPart.All(
            static c => char.IsLetterOrDigit(c) || c == '-' || c == '_')
        );
    }
    #endregion NextEmail
}
