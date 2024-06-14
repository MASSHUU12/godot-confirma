# Change Log

All notable changes to this project will be documented in this file.

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
