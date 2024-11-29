# Help pages structure
**Overview of JSON format**

[full structure](resources/help_page_structure.json)
---
file base template:

```json
{
    "version": 1,
    "data":[]
}
```
---

## text
- **text**: (string)
- **color**: (html color)
- **bg_color**: (html color)
- **format**:
    - **bold** (**b**)
    - **underline** (**u**)
    - **italic** (**i**)
    - **Strikethrough** (**s**)
    - **fill** (**f**)
    - **center** (**c**)
## header
- **text**: (string)
- **level** (1-4)

### example:
#### terminal:
![terminal](resources/terminal_header.png)
#### godot:
![godot](resources/godot_header.png)
```json
"data":[
    {
        "type":"header",
        "text":"level 1",
        "level":1
    },
        {
        "type":"header",
        "text":"level 2",
        "level":2
    },
        {
        "type":"header",
        "text":"level 3",
        "level":3
    },
        {
        "type":"header",
        "text":"level 4",
        "level":4
    }
]
```
## link
- **text**: (string)
- **url**: (valid url)

### example:
#### terminal:
![terminal](resources/terminal_link.png)
#### godot:
![godot](resources/godot_link.png)
```json
"data":[
    {
        "type":"link",
        "text":"text",
        "url": "https://example.com"
    }
]
```
## code
- **lines** (array of strings)

### example:
#### terminal:
![terminal](resources/terminal_code.png)
#### godot:
![godot](resources/godot_code.png)
```json
"data":[
    {
        "type":"code",
        "lines":[
            "line1",
            "line2",
            "line3"
        ]
    }
]
```

