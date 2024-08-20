# Change Log

All notable changes to this project will be documented in this file.

## [0.8.1-beta 2024-08-20]

### Fixed

- `NullReferenceException` when there is not GDScript tests.
- Confirma run tests and closes immediately after start instead of launching the game.

## [0.8.0-beta 2024-08-18]

### Added

- [**Experimental**] Information about number of orphans.
- [**Experimental**] Detecting when orphans are created.
- [**Experimental**] Running tests written in GDScript.
- 'GdAssertionFailed' signal to ConfirmaAutoload.
- Arguments:
  - "--confirma-disable-cs" to disable C# tests.
  - "--confirma-disable-gd" to disable GDScript tests.
  - "--confirma-gd-path" to specify path with GDScript tests.
  - "--confirma-output" specifies how to return test result information.
  - "--confirma-output-path" specifies the path
  in which to create a report of the tests performed.
- Classes:
  - ScriptInfo
  - TestManager
  - CsTestExecutor
  - GdTestDiscovery
  - TestLog
  - Json
  - EnumHelper
- Assertions for GDScript:
  - confirm.gd
  - confirm_boolean.gd
  - confirm_array.gd
- ITestExecutor interface.
- "Run C# Tests" and "Run GDScript Tests" buttons to editor bottom panel.
- ScriptMethodInfo, ScriptMethodReturnInfo & ScriptMethodArgumentsInfo records.
- TestLog list in TestResult and TestClassResult records.
- EscapeInvisibleCharacters string extension.
- Enums:
  - ELogType
  - ELogOutputType
  - ERunTargetType
- Alternative JSON output of test results.
- Total number of test classes to TestResult record.
- RunTarget struct and added it to TestsProps struct.

### Changed

- Color.TerminalReset is now static readonly field.
- Moved TestsExecutionStarted/Finished signals to TestRunner.
- The "Run All Tests" button is no longer available when tests are executed.
- Minor changes in exceptions messages.
- Code style improvements.
- Minor optimizations.
- Improved independence from culture.
- Renamed:
  - TestDiscovery -> CsTestDiscovery
  - TestBottomPanel -> ConfirmaBottomPanel
  - Class names are colored to help distinguish classes from tests.
- `Log.Print` prints to the stdio by default.
- Methods in `Log` and `Colors` are generic.
- Errors from `Log.PrintError` are printed to stderr instead of stdout.
- Argument '--confirma-method' no longer allows empty value.

### Removed

- TestExecutor.cs
- TestOutput.cs
- Argument "--confirma-quit".
- `ClassName` & `MethodName` from TestsProps.cs.

### Fixed

- Resource leak on Godot editor exit.
- Parallelizable tests log is out of order.
- Printed newline characters in arguments messes up the output.
- `ConfirmEqual`/`ConfirmNotEqual` for arrays passed as object
in case of an exception returns only the type instead of the actual values.
- `Log.Print` throws exception when used too early.
-  Argument '--confirma-run' with invalid class name
  throws a NullReferenceException.

## [0.7.1-beta 2024-07-24]

### Changed

- Confirma can operate from a location other than the default.

### Fixed

- Single tests are run twice.
- Issues occur when adding Confirma to the bottom panel of the editor when starting Godot.

## [0.7.0-beta 2024-07-23]

### Added

- '--confirma-method' argument allowing to run a single method.
- Flag to Repeat attribute to stop repeating after the first failed test.

### Changed

- Confirma uses invariant culture when working with strings & numbers.
- TestDiscovery class is now static.
- Renamed protected '_autoload' & '_output' fields to 'Autoload' & 'Output'.
- Repeat field in TestCase is now of type RepeatAttribute and not ushort.

## [0.6.1-beta 2024-07-08]

### Added

- Extensions:
  - NextDouble
  - NextElement
  - NextElements
  - NextShuffle
  - NextChar
  - NextLowerChar
  - NextUpperChar
  - NextDigitChar
  - NextString
  - ConfirmLowercase
  - ConfirmUppercase

## [0.6.0-beta 2024-07-02]

### Added

- Extensions:
  - ConfirmIsOdd
  - ConfirmIsEven
  - ConfirmCloseTo
- Extension classes:
  - RandomEnumExtensions
  - RandomBooleanExtensions
  - RandomNetworkExtensions
- Confirm class with assertions:
  - IsEnumValue
  - IsNotEnumValue
  - IsEnumName
  - IsNotEnumName
  - IsTrue
  - IsFalse
  - Throws
  - NotThrows
- Addon icon.

### Changed

- Numeric extensions now use 'INumber<T>' generic constraint
  instead of 'IComparable, IConvertible, IComparable<T>, IEquatable<T>'.

## [0.5.0-beta 2024-06-29]

### Added

- ConfirmMatchesPattern
- Extensions:
  - ConfirmSign
  - ConfirmMatchesPattern
  - ConfirmThrowsWMessage
  - ConfirmNotThrowsWMessage
  - ConfirmDoesNotMatchPattern
- Extension classes:
  - ConfirmUuidExtensions
  - ConfirmEventExtensions
  - ConfirmActionExtensions
  - ConfirmDateTimeExtensions
  - ConfirmReferenceExtensions
- Simple library for generating random:
  - Numbers
  - UUIDs
- Assertion chaining.
- Repeat attribute.

### Changed

- Improved default messages.

### Fixed

- Typo in ConfirmRangeExtensions.

## [0.4.2-beta 2024-06-14]

### Changed

- Parallel execution of tests has been improved, significantly increasing performance.

## [0.4.1-beta 2024-05-20]

### Added

- Message parameter to all extensions.
- Ignore attribute now accepts EIgnoreMode enum
  which allows ignoring class/method always or when ran from editor.

### Changed

- Improved display of passed parameters to the methods.
- Improved default exception messages.

## [0.4.0-beta 2024-05-06]

### Added

- Static Global class which simplifies access for the tests to the scene tree.
- Assertions for IEnumerable & attributes.

### Changed

- Simplified checking if Action/Func throws an exception.

## [0.3.0-beta 2024-05-03]

### Added

- Default, more concise way of displaying information in the terminal.
- Allow running tests in verbose mode via editor.
- '--confirma-verbose' argument.
- '--confirma-sequential' argument.

### Changed

- The old way of displaying information is available under the '--confirma-verbose' argument.
- Moved extensions from Confirma namespace to Confirma.Extensions.

### Fixed

- TestRunner in editor doesn't work.
- 'Node not found: "/root/Confirma"' error in the editor when starting the project.

## [0.2.0-beta 2024-04-30]

### Added

- '--confirma-exit-on-failure' argument.
- run_tests.sh script.
- Classes:
  - Converter
  - VectorExtensions
  - VariantExtensions
  - ConfirmFileExtensions
  - ConfirmVectorExtensions
  - ConfirmSignalExtensions
  - ConfirmNumericExtensions
  - ConfirmDictionaryExtensions
- String extensions:
  - ConfirmNotHasLength
  - ConfirmEqualsCaseInsensitive
  - ConfirmNotEqualsCaseInsensitive

### Changed

- Moved ConfirmAssert exception to Confirma.Exceptions namespace.

## [0.1.0-beta 2024-04-23]

Initial release.

## [0.0.0 2024-04-04]

Start of the project.
