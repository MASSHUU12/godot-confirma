class_name ConfirmEqualWrapperTest


func confirm_equal_when_equal() -> void:
	ConfirmEqual.equal(5, 5)
	ConfirmEqual.equal("Hello", "Hello")
	ConfirmEqual.equal(5.5, 5.5)


func confirm_not_equal_when_not_equal() -> void:
	ConfirmEqual.not_equal(5, 6)
	ConfirmEqual.not_equal("Hello", "World")
	ConfirmEqual.equal(5.5, 5.6)
