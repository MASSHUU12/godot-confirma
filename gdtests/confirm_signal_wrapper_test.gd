class_name ConfirmSignalWrapperTest

func confirm_signal_exists_when_exists() -> void:
	var btn := Button.new()

	ConfirmSignal.exists(btn, "pressed")

	btn.free()


func confirm_signal_does_not_exist_when_does_not_exist() -> void:
	var btn := Button.new()

	ConfirmSignal.does_not_exist(btn, "pressed_heavily")

	btn.free()
