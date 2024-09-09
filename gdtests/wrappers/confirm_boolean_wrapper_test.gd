class_name ConfirmBooleanWrapperTest extends TestClass

func category() -> String:
	return "Hi"


func ignore() -> Ignore:
	return Ignore.new(2, "", true, "Hi")


func confirm_true_when_true() -> void:
	ConfirmBoolean.is_true(true)


func confirm_false_when_false() -> void:
	ConfirmBoolean.is_false(false)
