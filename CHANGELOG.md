# Change Log

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added

- Script templates:
  - Test.cs
  - test.gd
  - TestFull.cs
  - test_full.gd
- Classes:
  - Mock
  - CallRecord
  - FuzzGenerator
  - MethodInfoExtensions
- Assertions:
  - ConfirmInstanceOf
  - ConfirmNotInstanceOf
  - ConfirmIsNaN
  - ConfirmIsNotNaN
  - ConfirmElementsAreOrdered w/ comparer
  - ConfirmElementsAreNotOrdered w/ & w/o comparer
- Enums:
  - EDistributionType
- Attributes:
  - FuzzAttribute
- Random extensions:
  - NextGaussianDouble
  - NextExponentialDouble
  - NextPoissonInt
- Script test.sh.
- Library allowing to mock interfaces and classes.
- Support for flaky tests.
- Ability to ignore tests in headless mode.
- Support for test methods with `params` modifier.
- Flag to `CollectionHelper.ToString` & `CollectionFormatter` for adding type hint.

### Changed

- Updated documentation:
  - Added info in TESTING.md about script templates, mocking library,
flaky tests, fuzz testing & `params` modifier.
  - Added info about mocking libary in README.md.
  - Updated docs about `Repeat` and `Ignore` attribute.
- Allowed the same numbers for min & max values in `NextDecimal`, `NextLong` &
`NextDouble` extensions of `Random` class.
- Adjusted warnings for `Repeat` attribute.
- Adjusted `Repeat` attribute to handle flaky tests.
- Optimized `ConfirmElementsAreOrdered<T>` and `ConfirmElementsAreEquivalent<T>`
in `ConfirmIEnumerableExtensions`.
- String representation of arrays also displays their type.

### Removed

- '>' character from test output.
- Script run_tests.sh.
- `GetTestCasesFromMethod` from CsTestDiscovery.cs.

### Fixed

- [#213] [GDScript] Method names in verbose mode display "([])"
when there are no arguments
- [#216] [GDScript] `System.IO.DirectoryNotFoundException`
thrown when passing invalid path to '--confirma-gd-path'.
- [#221] [GDScript] An error is thrown when path to GDScript tests is empty.
- [#221] [GDScript] An incorrect error is thrown when path to GDScript tests is
invalid.
- [#225] Method name in verbose mode has additional space before pipe sign.
- `CollectionHelper.ToString` sometimes added unnecessary parentheses.

## [0.9.0-beta 2024.10.21]

### Added

- Arguments:
  - '--confirma-category'
  - '--confirma-disable-orphans-monitor'
- Formatters:
  - AutomaticFormatter
  - DefaultFormatter
  - NumericFormatter
  - StringFormatter
  - CollectionFormatter
- Wrappers for C# assertions for GDScript:
  - ConfirmEqualWrapper
  - ConfirmArrayWrapper
  - ConfirmRangeWrapper
  - ConfirmSignalWrapper
  - ConfirmStringWrapper
  - ConfirmVectorWrapper
  - ConfirmBooleanWrapper
  - ConfirmNumericWrapper
  - ConfirmDictionaryWrapper
- C# classes:
  - WrapperBase
  - GdScriptInfo
  - Settings
  - ConfirmaBotomPanelOptions
  - AssertionMessageGenerator
  - CollectionHelper
  - Formatter
  - TreeContent
- GDScript classes:
  - TestClass
  - Ignore
  - RunTarget
- Support for lifecycle methods in GDScript:
  - after_all
  - before_all
  - set_up
  - tear_down
  - category
  - ignore
- Documentation for:
  - settings
  - testing GDScript code
  - custom assertions
- Project settings:
  - GDScript Tests Folder
  - Output Path
- Bottom panel settings:
  - [#178] Allow changing category.
  - Allow disabling parallelization.
  - Allow disabling orphans monitor.
  - Allow disabling parallelization.
  - Allow changing output type & path.
- Warning when trying to run tests without '--confirma-run' argument.
- `WhenNotRunningCategory` value to `EIgnoreMode` enum.
- `HideFromResults` field to `Ignore` attribute.
- `IFormatter` interface.
- Constructor to `ConfirmAssertException` which automatically generates a message.
- `ELifecycleMethodName` enum.
- A clear distinction between C# and GDScript tests when displaying results.
- New bottom panel settings window.
- Base class for lifecycle attributes (`LifecycleAttribute`).
- Allow to disable exterior brackets (`CollectionHelper.ToString`).
- `LifecycleMethodException` exception.
- Overload of `TestResult`'s `+` attribute for `TestMethodResult`.
- Support for non-static test classes.

### Changed

- Included 'scripts' folder in exported ZIP.
- Documentation has been refreshed (more information, better described).
- Adjusted most of the assertion messages.
- Adjusted GDScript assertions to use C# wrappers.
- [#170] Recursively check every folder for GDScript tests (max depth is 16).
- All GDScript test classes need to extend `TestClass`.
- `GdTestDiscovery` & `GdTestExecutor` now use `GdScriptInfo` instead of `ScriptInfo`.
- GDScript tests folder can be passed as global or localized path.
- `Print` and `PrintLine` methods from `Log` class support list of arguments.
- Moved verbose switch to new settings window.
- From now on, lifecycle attributes are assigned to a class,
and not to a specific method, and take the name of the method to run.
One attribute of a given type is allowed.
- Replaced `ArrayHelper.ToString` with `CollectionHelper.ToString`.
- [#188] Failed invocation of lifecycle method results in test failure.
- Orphans monitor is no longer considered experimental and is enabled by default.
- `ConfirmaBottomPanelOptions` uses `TreeContent` for generating window content.

### Removed

- `GdAssertionFailed` signal.
- `LifecycleMethodData` record.
- `ArrayHelper` class.
- `ArrayHelperTest` Test.
- '--experimental-monitor-orphans' argument.

### Fixed

- Empty argument '--confirma-method' was allowed
when argument '--confirma-run' was also empty.
- Not every GDScript test is shown in results.

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
- Argument '--confirma-run' with invalid class name
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
