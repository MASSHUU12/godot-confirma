# Arguments

| Argument                       | Description                                                                                        |
| ------------------------------ | -------------------------------------------------------------------------------------------------- |
| --confirma-run                 | Launches tests, allows specifying class name after '=' to run only this class.                     |
| --confirma-method              | Launches single test, requires a method name after '=' and '--confirma-run' with the target class. |
| --confirma-category            | Launches tests located in the specified category.                                                  |
| --confirma-verbose             | Displays more information available about the tests being run.                                     |
| --confirma-sequential          | Disables parallelization.                                                                          |
| --confirma-exit-on-failure     | Terminates after the first error occurs.                                                           |
| --experimental-monitor-orphans | [Experimental] Allows monitoring the number of orphans, and when they arise.                       |
| --confirma-disable-cs          | Disables C# tests.                                                                                 |
| --confirma-disable-gd          | Disables GDScript tests.                                                                           |
| --confirma-gd-path             | Specifies the path where GDScript tests are located, the default path is "./gdtests".              |
| --confirma-output              | Specifies how to return tests result information (log and/or JSON).                                |
| --confirma-output-path         | Specifies the path in which to create a report of the tests performed (directories must exist).    |
| --headless                     | Runs Godot in server mode (without windows), an argument built into Godot.                         |

## Passing arguments

Arguments can be passed to Godot as follows:

```txt
<godot executable> <built-in args> -- <confirma args>
```

Example:

```bash
$GODOT --headless -- --confirma-run=AwesomeClassTest --confirma-method=TrickyMethodTest
```
