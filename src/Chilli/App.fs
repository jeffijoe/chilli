// Chilli - helps you rename season episodes.
// .. get it? Because seasons.. and Chilli is a seasoning.. GET IT?
// 
// Created by Jeff Hansen - Jeffijoe.com
// p.s. this was my first F# program

module App

open System
open System.IO
open Utils

// Shortcut for safely grabbing arguments.
let getArg (argv: string[]) index = if argv.Length > index then argv.[index] else null

[<EntryPoint>]
let main (argv: string[]) =
    printfn "Chilli - episode renamer because you have better things to do"
    printfn "Usage: chilli [directory] pattern [--dry-run]"
    printfn "  directory: The directory containing files to rename. Defaults to current working directory."
    printfn "  pattern: E.g. Pokemon S01E$EPNUM$.mp4 will rename \"Pokemon S01E20.mp4\" to \"020.mp4\"."
    printfn "  --dry-run: Only prints what will happen, but won't actually rename anything."
    printfn ""
        
    // Currying ftw
    let arg = getArg argv
    // How expressive this is!
    let arg1 = arg 0
    let arg2 = arg 1
    let arg3 = arg 2

    let dryRun = arg2 = "--dry-run" || arg3 = "--dry-run"
    if dryRun then printfn "(Dry run active, won't actually rename anything)"
    let firstArgIsPath = arg1 <> null && Directory.Exists arg1
    let path = if firstArgIsPath then arg1 else Environment.CurrentDirectory
    
    let template = if firstArgIsPath then arg2 else arg1
    if template = null then
        printfn "No pattern passed in - example: Pokemon S01E$EPNUM$.mp4"
        1
    else
        processFiles path template dryRun
        0
