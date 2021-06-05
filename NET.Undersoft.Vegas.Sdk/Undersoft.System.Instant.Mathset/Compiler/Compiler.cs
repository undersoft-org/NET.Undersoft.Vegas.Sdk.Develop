﻿using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Instant.Mathset
{
    public class Compiler
    {
        static ModuleBuilder MODULE;
        static AssemblyBuilder ASSEMBLY;
        static int CLASS_ID;
        static bool COLLECT_MODE = false;       // optional
        static string TYPE_PREFIX = "ENTER_THE_MATHLINE";
        static string EXE_NAME = "GENERATED_CODE";

        public static bool CollectMode
        {
            get { return COLLECT_MODE; }
            set
            {
                if (MODULE == null)
                    COLLECT_MODE = value;
                else
                {
                    throw new Exception("SaveMode cannot be more Changed!");
                }
            }
        }

        public static CombinedMathset Compile(Formula formula)
        {
            if (MODULE == null)
            {
                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "EmittedAssembly";
               
                ASSEMBLY = AssemblyBuilder.DefineDynamicAssembly(assemblyName, CollectMode ? AssemblyBuilderAccess.RunAndCollect : AssemblyBuilderAccess.Run);              
                MODULE = ASSEMBLY.DefineDynamicModule("EmittedModule");
                CLASS_ID = 0;
            }
            CLASS_ID++;

            TypeBuilder MathsetFormula = MODULE.DefineType(TYPE_PREFIX + CLASS_ID, TypeAttributes.Public, typeof(CombinedMathset));
            Type[] constructorArgs = { };
            ConstructorBuilder constructor = MathsetFormula.DefineConstructor(
                MethodAttributes.Public, CallingConventions.Standard, null);

            ILGenerator constructorIL = constructor.GetILGenerator();
            constructorIL.Emit(OpCodes.Ldarg_0);
            ConstructorInfo superConstructor = typeof(Object).GetConstructor(new Type[0]);
            constructorIL.Emit(OpCodes.Call, superConstructor);
            constructorIL.Emit(OpCodes.Ret);

            Type[] args = { };
            MethodBuilder fxMethod = MathsetFormula.DefineMethod("Compute", MethodAttributes.Public | MethodAttributes.Virtual, typeof(void), args);
            ILGenerator methodIL = fxMethod.GetILGenerator();
            CompilerContext context = new CompilerContext();

            // first pass calculate the parameters			
            // initialize and declare the parameters, start with 
            // localVectorI = parameters[i];			
            // next pass implements the function
            formula.Compile(methodIL, context);
            context.NextPass();
            context.GenerateLocalInit(methodIL);
            formula.Compile(methodIL, context);

            // finally return
            methodIL.Emit(OpCodes.Ret);

            // create the class...
            Type mxt = MathsetFormula.CreateTypeInfo();
            CombinedMathset computation = (CombinedMathset)Activator.CreateInstance(mxt, new Object[] { });
            computation.SetParams(context.ParamCards, context.Count);

            return computation;
        }       
    }
}