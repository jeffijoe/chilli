// Chilli - helps you rename season episodes.
// .. get it? Because seasons.. and Chilli is a seasoning.. GET IT?
// 
// Created by Jeff Hansen - Jeffijoe.com
// p.s. this was my first F# program

open System.IO
open System
open System.Text.RegularExpressions

// Given the template, extracts the episode number
let getEpisodeNumber (template: Regex) str =
    template.Match(str).Groups.[1].Value

// Left-pads a string
let padLeft (number: int) (padChar: char) (str: string) =
    str.PadLeft(number, padChar)

let getNewFilename oldFilename episodeNumber =
    let extension = Path.GetExtension oldFilename
    let episodeNumberAsString = episodeNumber.ToString()
                    
    if String.IsNullOrEmpty episodeNumberAsString then
        oldFilename
    else
        episodeNumberAsString
            |> padLeft 3 '0'
            |> fun x -> x + extension

// Renames a file based on episode number
let renameFile oldFilename episodeNumber dryRun =
    let newFilename = getNewFilename oldFilename episodeNumber
    if oldFilename <> newFilename then
        printfn "Rename \"%s\" to \"%s\"" oldFilename newFilename
        if dryRun = false then
            File.Move(oldFilename, newFilename)

// Makes a template regex
let makeTemplate input = 
    input 
    |> Regex.Escape
    |> fun x -> Regex(@"\\\$EPNUM\\\$").Replace(x, "(\d*)")
    |> Regex

// Gets filenames from a path.
let getFilenames path =
    let dir = DirectoryInfo(path)
    dir.EnumerateFiles() |> Seq.map(fun x -> x.Name)

let processFiles path templateString dryRun =
    let files =  getFilenames path        
    let template = makeTemplate templateString
    
    let processFile file =
        let episodeNumber = getEpisodeNumber template file
        renameFile file episodeNumber dryRun

    files |> Seq.iter processFile

let getArg (argv: string[]) index =
    if argv.Length > index then argv.[index] else null

[<EntryPoint>]
let main (argv: string[]) =
    printfn "Chilli - episode renamer because you have better things to do"
    printfn "Usage: chilli [directory] pattern"
    printfn "  directory: The directory containing files to rename. Defaults to current working directory."
    printfn "  pattern: The filename pattern so Chilli knows how to rename. E.g. Pokemon S01E$EPNUM$.mp4 will rename \"Pokemon S01E20.mp4\" to \"020.mp4\"."
    printfn ""
    
    let arg = getArg argv
    let arg1 = arg 0
    let arg2 = arg 1
    let arg3 = arg 2

    let dryRun = arg3 = "--dry-run"
    if dryRun then printfn "(Dry run active, wont actually rename anything)"
    
    let firstArgIsPath = arg1 <> null && Directory.Exists arg1
    let path =
        if firstArgIsPath then
            arg1
        else 
            Environment.CurrentDirectory
    
    let template = if firstArgIsPath then arg2 else arg1
    if template = null then
        printfn "No pattern passed in - example: Pokemon S01E$EPNUM$.mp4"
        

    processFiles path template dryRun

    // Return an error code.
    0
