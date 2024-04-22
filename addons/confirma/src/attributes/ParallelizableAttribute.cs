using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ParallelizableAttribute : Attribute
{
	public ushort Threads { get; }

	public ParallelizableAttribute()
	{
		Threads = Math.Min((ushort)2, (ushort)Environment.ProcessorCount);
	}

	public ParallelizableAttribute(ushort threads)
	{
		Threads = ValidateThreads(threads);
	}

	private static ushort ValidateThreads(ushort threads)
	{
		return Math.Min(
			Math.Max((ushort)1, threads),
			(ushort)Environment.ProcessorCount
		);
	}
}
