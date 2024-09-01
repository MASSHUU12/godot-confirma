class_name ConfirmDictionaryWrapperTest


const TEST_DICT := {
	"Hello": "World",
	1: 2
}


func confirm_contains_key_when_contains_key() -> void:
	ConfirmDictionary.contains_key(TEST_DICT, "Hello")


func confirm_not_contains_key_when_not_contains_key() -> void:
	ConfirmDictionary.not_contains_key(TEST_DICT, 3)


func confirm_contains_value_when_contains_value() -> void:
	ConfirmDictionary.contains_value(TEST_DICT, 2)


func confirm_not_contains_value_when_not_contains_value() -> void:
	ConfirmDictionary.not_contains_value(TEST_DICT, 3)


func confirm_contains_key_value_pair_when_contains() -> void:
	ConfirmDictionary.contains_key_value_pair(TEST_DICT, "Hello", "World")


func confirm_not_contains_key_value_pair_when_not_contains() -> void:
	ConfirmDictionary.not_contains_key_value_pair(TEST_DICT, "Hello", "Human")
