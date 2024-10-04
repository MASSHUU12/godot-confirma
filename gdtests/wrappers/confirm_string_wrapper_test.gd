class_name ConfirmStringWrapperTest extends TestClass

func confirm_empty_when_empty() -> void:
	ConfirmString.empty("")


func confirm_not_empty_when_not_empty() -> void:
	ConfirmString.not_empty("Hello")


func confirm_contains_when_contains() -> void:
	ConfirmString.contains("Hello, World!", "World")


func confirm_not_contains_when_not_contains() -> void:
	ConfirmString.not_contains("Hello, World!", "Hooman")


func confirm_starts_with_when_starts_with() -> void:
	ConfirmString.starts_with("Hello, World!", "Hello")


func confirm_not_starts_with_when_not_starts_with() -> void:
	ConfirmString.not_starts_with("Hello, World!", "Hi")


func confirm_ends_with_when_ends_with() -> void:
	ConfirmString.ends_with("Hello, World!", "World!")


func confirm_not_ends_with_when_not_ends_with() -> void:
	ConfirmString.not_ends_with("Hello, World!", "Human")


func confirm_has_length_when_has_length() -> void:
	ConfirmString.has_length("Hello, World!", 13)


func confirm_not_has_length_when_not_has_length() -> void:
	ConfirmString.not_has_length("Hello, World!", 12)


func confirm_equals_case_insensitive_when_equals_case_insensitive() -> void:
	ConfirmString.equals_case_insensitive(
		"Hello, World!",
		"heLLo, WORLD!"
	)


func confirm_not_equals_case_i_when_not_equals_case_i() -> void:
	ConfirmString.not_equals_case_insensitive(
		"Hello, World!",
		"heLLo, H00MAN!"
	)


func confirm_matches_pattern_when_matches_pattern() -> void:
	ConfirmString.matches_pattern(
		"Hello, World!",
		"^\\w+, \\w+!$"
	)


func confirm_does_not_matches_pattern_when_does_not_matches_pattern() -> void:
	ConfirmString.does_not_match_pattern(
		"Hello World!",
		"^\\w+, \\w+!$"
	)


func confirm_lowercase_when_lowercase() -> void:
	ConfirmString.lowercase("hello, world!")


func confirm_uppercase_when_uppercase() -> void:
	ConfirmString.uppercase("HELLO, WORLD!")
