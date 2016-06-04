#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Testing

let outputFolder = "artifacts"

let getConfiguration = fun () ->
    match getBuildParam "target" with
    | "Release" | "Publish" -> "Release"
    | _                     -> "Debug"

Target "Clean" (fun _ ->
    CleanDir outputFolder
)

Target "Restore" (fun _ ->
    RestorePackages ()
)

Target "Build" (fun _ ->
    !! "*.sln"
    |> MSBuild outputFolder "Rebuild" [ "Configuration", getConfiguration () ] |> ignore
)

Target "UnitTests" (fun _ ->
    !! (outputFolder + "/*.Tests.dll")
    |> xUnit2 (fun p -> { p with Parallel = ParallelMode.All
                                 HtmlOutputPath = Some (outputFolder @@ "Tests.html") })
)

Target "IntegrationTests" (fun _ ->
    !! (outputFolder + "/*.IntegrationTests.dll")
    |> xUnit2 (fun p -> { p with Parallel = ParallelMode.All
                                 HtmlOutputPath = Some (outputFolder @@ "IntegrationTests.html") })
)

Target "NugetPackage" (fun _ ->
    !! "*.nuspec"
    |> Seq.iter (NuGet (fun p -> { p with Version = GetAssemblyVersionString (outputFolder @@ "CodeContracts.Fody.dll")
                                          WorkingDir = outputFolder
                                          OutputPath = outputFolder }))
)

Target "NugetPublish" (fun _ ->
    ()
)

Target "Debug" ignore

Target "Release" ignore

Target "Publish" ignore

"Clean" ==> "Build"

"Restore" ==> "Build"

"Build" ==> "UnitTests"
"Build" ==> "integrationTests"

"UnitTests" ==> "NugetPackage"
"UnitTests" ==> "Debug"

"IntegrationTests" ==> "NugetPackage"
"IntegrationTests" ==> "Debug"

"NugetPackage" ==> "NugetPublish"
"NugetPackage" ==> "Release"

"NugetPublish" ==> "Publish"

RunTargetOrDefault "Debug"
