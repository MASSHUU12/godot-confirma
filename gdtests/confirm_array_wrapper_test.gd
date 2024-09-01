class_name ConfirmArrayWrapperTest


func confirm_is_of_size_when_is_of_size() -> void:
	ConfirmArray.is_of_size([], 0)
	ConfirmArray.is_of_size([1], 1)
	ConfirmArray.is_of_size([1, 1], 2)


func confirm_is_empty_when_is_empty() -> void:
	ConfirmArray.is_empty([])


func confirm_is_not_empty_when_is_not_empty() -> void:
	ConfirmArray.is_not_empty([null])
	ConfirmArray.is_not_empty([1])
	ConfirmArray.is_not_empty([1, 1])


func confirm_contains_when_contains() -> void:
	ConfirmArray.contains([1], 1)
	ConfirmArray.contains([1, "Hello", "World"], "World")
	ConfirmArray.contains([1, [2]], [2])


func confirm_not_contains_when_not_contains() -> void:
	ConfirmArray.not_contains([1], 2)
	ConfirmArray.not_contains([1, "Hello", "World"], "!")
	ConfirmArray.not_contains([1, [2]], [3])
