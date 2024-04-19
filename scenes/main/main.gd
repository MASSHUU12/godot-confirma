extends Control

@onready var btn: Button = get_node("CenterContainer/Panel/MarginContainer/VBoxContainer/Button")

func _ready() -> void:
	btn.connect("pressed", _on_button_pressed)

func _on_button_pressed():
	get_tree().change_scene_to_packed(load("uid://cq76c14wl2ti3"))
