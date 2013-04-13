/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using Mosa.Compiler.Framework.Stages;
using Mosa.Compiler.TypeSystem;

namespace Mosa.Compiler.Framework.Linker
{
	public sealed class LinkerMethodCompiler : BaseMethodCompiler
	{
		#region Construction

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkerMethodCompiler"/> class.
		/// </summary>
		/// <param name="compiler">The assembly compiler executing this method compiler.</param>
		/// <param name="method">The metadata of the method to compile.</param>
		/// <param name="instructionSet">The instruction set.</param>
		public LinkerMethodCompiler(BaseCompiler compiler, RuntimeMethod method, InstructionSet instructionSet)
			: base(compiler, method, instructionSet)
		{
			Context context = ContextHelper.CreateNewBlockWithContext(instructionSet, this.BasicBlocks, BasicBlock.PrologueLabel);
			BasicBlocks.AddHeaderBlock(context.BasicBlock);

			this.Pipeline.AddRange(new IMethodCompilerStage[] {
				new StackSetupStage(),
				new LoopAwareBlockOrderStage(),
				new PlatformStubStage(),
				new StackLayoutStage(),

				//new CodeGenerationStage(),
			});

			compiler.Architecture.ExtendMethodCompilerPipeline(this.Pipeline);
		}

		#endregion Construction
	}
}