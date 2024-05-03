# Change Log

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added

- Abstract TestClass allowing access for the tests to the scene tree.

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
