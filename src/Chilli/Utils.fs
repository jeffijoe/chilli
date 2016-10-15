module Utils

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

// Renames the files.
let processFiles path templateString dryRun =
    let files =  getFilenames path        
    let template = makeTemplate templateString
    
    let processFile file =
        let episodeNumber = getEpisodeNumber template file
        renameFile file episodeNumber dryRun

    files |> Seq.iter processFile
