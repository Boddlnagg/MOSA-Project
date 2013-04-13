﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Simon Wollwage (rootnode) <kintaro@think-in-co.de>
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System;
using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// Representations the x86 adc instruction.
	/// </summary>
	public sealed class Adc : X86Instruction
	{
		#region Data members

		private static readonly OpCode R_C = new OpCode(new byte[] { 0x81 }, 2);
		private static readonly OpCode M_C = R_C;
		private static readonly OpCode R_R = new OpCode(new byte[] { 0x11 });
		private static readonly OpCode M_R = R_R;
		private static readonly OpCode R_M = new OpCode(new byte[] { 0x13 });

		#endregion Data members

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="Adc"/>.
		/// </summary>
		public Adc() :
			base(1, 2)
		{
		}

		#endregion Construction

		#region Methods

		/// <summary>
		/// Computes the opcode.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <param name="source">The source operand.</param>
		/// <param name="third">The third operand.</param>
		/// <returns></returns>
		protected override OpCode ComputeOpCode(Operand destination, Operand source, Operand third)
		{
			if (destination.IsRegister && third.IsConstant) return R_C;
			if (destination.IsRegister && third.IsRegister) return R_R;
			if (destination.IsRegister && third.IsMemoryAddress) return R_M;
			if (destination.IsMemoryAddress && third.IsRegister) return M_R;
			if (destination.IsMemoryAddress && third.IsConstant) return M_C;

			throw new ArgumentException(@"No opcode for operand type.");
		}

		/// <summary>
		/// Allows visitor based dispatch for this instruction object.
		/// </summary>
		/// <param name="visitor">The visitor object.</param>
		/// <param name="context">The context.</param>
		public override void Visit(IX86Visitor visitor, Context context)
		{
			visitor.Adc(context);
		}

		#endregion Methods
	}
}