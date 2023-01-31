GherkInspector
==============
GherkInspector is a tool to help inspect your Gherkin/feature files and provide useful metrics - such as the number of tests or usages of a step or tag - as well as compare the Gherkin syntax itself against a set of style rules. These features and more will be built into the tool over time.

## Example usage
Use the Run.cmd file or you can call dotnet run like this

```console
dotnet run --project "GherkInspector.Console\GherkInspector.Console.csproj" -- "Features"
```


**Output**
```console
   _____ _               _    _____                           _
  / ____| |             | |  |_   _|                         | |
 | |  __| |__   ___ _ __| | __ | |  _ __  ___ _ __   ___  ___| |_ ___  _ __
 | | |_ | '_ \ / _ \ '__| |/ / | | | '_ \/ __| '_ \ / _ \/ __| __/ _ \| '__|
 | |__| | | | |  __/ |  |   < _| |_| | | \__ \ |_) |  __/ (__| || (_) | |
  \_____|_| |_|\___|_|  |_|\_\_____|_| |_|___/ .__/ \___|\___|\__\___/|_|
                                             | |
                                             |_|


Find feature files recursively, start at: Features
    > Features\Simple.feature
```