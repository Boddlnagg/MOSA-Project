﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Simon Wollwage (rootnode) <kintaro@think-in-co.de>
 */

using Mosa.Compiler.Framework;
using Mosa.Compiler.Metadata.Signatures;
using Mosa.Compiler.Framework.IR;

namespace Mosa.Platform.x86.Intrinsic
{
	/// <summary>
	///
	/// </summary>
	internal class GetControlRegisterBase : IIntrinsicPlatformMethod
	{
		private ControlRegister control;

		/// <summary>
		/// Initializes a new instance of the <see cref="GetControlRegisterBase"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		protected GetControlRegisterBase(ControlRegister control)
		{
			this.control = control;
		}

		#region Methods

		/// <summary>
		/// Replaces the intrinsic call site
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="typeSystem">The type system.</param>
		void IIntrinsicPlatformMethod.ReplaceIntrinsicCall(Context context, BaseMethodCompiler methodCompiler)
		{
			Operand result = context.Result;
			Operand imm = methodCompiler.CreateVirtualRegister(BuiltInSigType.UInt32);

			context.SetInstruction(IRInstruction.Move, imm, Operand.CreateCPURegister(BuiltInSigType.UInt32, control));
			context.AppendInstruction(IRInstruction.Move, result, imm);
		}

		#endregion Methods
	}
}