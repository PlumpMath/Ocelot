namespace Ocelot.Args

open Argu

[<CliPrefix(CliPrefix.DoubleDash)>]
[<NoAppSettings>]
type OcelotArguments =
    | [<Mandatory>] [<AltCommandLine("-f")>] File of path:string
    
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | File _ -> "Specify the file containing the assembly to analyze."

type ParseResult =
    | Success of ParseResults<OcelotArguments>
    | Error of string

type Options(args:string[], program:string) = 
    let parser  = ArgumentParser.Create<OcelotArguments>(programName = program)
    let parse_results = 
        try
            let result = parser.Parse args
            Some result
        with 
            | :? ArguParseException as e -> 
            None
    
    member this.ParseSucceded = 
        match parse_results with
        | Some r -> true
        | None -> false
    
    member this.File = 
        match parse_results with
        | Some r -> r.GetResult <@File@>
        | None -> ""        
    
    member this.Usage = parser.PrintUsage()