open System
open System.IO

//
// Make a working copy
//
let XAMARIN_MACIOS = "/Users/fak/Projects/xamarin-macios"

let origPath =
    Path.Combine
        (XAMARIN_MACIOS,
         "_maccat-build/Library/Frameworks/Xamarin.iOS.framework/Versions/git/lib/64bits/Xamarin.iOS.dll")

let workingPath = Path.Combine(Path.GetTempPath(), "Xamarin.iOS.dll")

File.Delete(workingPath)
File.Copy(origPath, workingPath, overwrite = true)

File.Exists(workingPath)

//
// Open the assembly
//
#r "/Users/fak/.nuget/packages/mono.cecil/0.11.2/lib/netstandard2.0/Mono.Cecil.dll"
#r "/Users/fak/.nuget/packages/mono.cecil/0.11.2/lib/netstandard2.0/Mono.Cecil.Rocks.dll"

open Mono.Cecil
open Mono.Cecil.Rocks
open Mono.Cecil.Cil

let asmStream = File.Open(workingPath, FileMode.Open, FileAccess.ReadWrite)
let asm = AssemblyDefinition.ReadAssembly(asmStream)

let types = asm.MainModule.Types.ToArray()

let t = types |> Seq.find (fun x -> x.Name = "UIImageResizingMode")

let ms =
    (types |> Seq.find (fun x -> x.Name = "UIImage")).Methods
    |> Seq.map (fun x -> x.Name)
    |> Array.ofSeq


//
// Look for methods that use the enum things
//

let isInterestingEnum (t : TypeReference) =
    t.FullName = "UIKit.UITextAlignment" || t.FullName = "UIKit.UIImageResizingMode"

let methodUsesEnum (m : MethodDefinition) =
    isInterestingEnum m.ReturnType || m.Parameters |> Seq.exists (fun p -> isInterestingEnum p.ParameterType)


let usesEnum (t : TypeDefinition) =
    t.Methods
    |> Seq.filter (fun x -> x.HasBody)
    |> Seq.exists methodUsesEnum

let typesThatUseTheEnum =
    types
    |> Seq.filter usesEnum
    |> Array.ofSeq

//
// Modify the methods
//


// ldarg.3
// ...
//
//
// start:   ldarg.3
//          bne 1    test_2:
//          ld  2
//          b        end:
// test_2:  ldarg.3
//          bne 2    reload:
//          ld 1
//          b        end:
// reload:  ldarg.3
// end:     ..

let modifyParameterValues (method : MethodDefinition) (param : ParameterDefinition) =
    //printfn "MODIFYING PARAM %s.%s/%s" method.DeclaringType.Name method.Name param.Name

    let body = method.Body
    let il = body.GetILProcessor()

    let mutable ip = 0

    let argIndex =
        if method.IsStatic then param.Index
        else param.Index + 1
    //printfn "! %s = ldarg.%O" param.Name argIndex

    while ip < body.Instructions.Count do
        let ins = body.Instructions.[ip]

        let isLdarg =
            match ins.OpCode.Code with
            | Mono.Cecil.Cil.Code.Ldarg_0 -> 0 = argIndex
            | Mono.Cecil.Cil.Code.Ldarg_1 -> 1 = argIndex
            | Mono.Cecil.Cil.Code.Ldarg_2 -> 2 = argIndex
            | Mono.Cecil.Cil.Code.Ldarg_3 -> 3 = argIndex
            | Mono.Cecil.Cil.Code.Ldarg_S -> System.Object.ReferenceEquals(ins.Operand, param)
            | Mono.Cecil.Cil.Code.Ldarg -> (ins.Operand :?> int) = argIndex
            | _ -> false
        if isLdarg then
            let clone() = il.Create(OpCodes.Ldarg_S, param)
            let start = clone()
            let test2 = clone()
            let reload = ins
            let endi = ins.Next

            let code =
                [| start
                   il.Create(OpCodes.Ldc_I8, 1L)
                   il.Create(OpCodes.Bne_Un, test2)
                   il.Create(OpCodes.Ldc_I8, 2L)
                   il.Create(OpCodes.Br, endi)
                   test2
                   il.Create(OpCodes.Ldc_I8, 2L)
                   il.Create(OpCodes.Bne_Un, reload)
                   il.Create(OpCodes.Ldc_I8, 1L)
                   il.Create(OpCodes.Br, endi)
                |]
            for c in code do
                il.InsertBefore(ins, c)
            //printfn "  FOUND  %O" ins
            //printfn "  FOUND  %O\n%A"  ins code
            ip <- ip + code.Length + 1
        else ip <- ip + 1

    body.MaxStackSize <- body.MaxStackSize + 1
    body.Optimize()

let modifyType (t : TypeDefinition) =
    for m in t.Methods do
        for p in m.Parameters do
            if isInterestingEnum p.ParameterType then modifyParameterValues m p

typesThatUseTheEnum |> Seq.iter modifyType


asm.Write()
