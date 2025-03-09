import os
import json

translit_map = {
    "a": 'а', "b": 'б', "v": 'в', "g": 'г', "d": 'д',
    "e": 'е', "yo": 'ё', "zh": 'ж', "z": 'з', "i": 'и',
    "y": 'й', "k": 'к', "l": 'л', "m": 'м', "n": 'н',
    "o": 'о', "p": 'п', "r": 'р', "s": 'с', "t": 'т',
    "u": 'у', "f": 'ф', "kh": 'х', "ts": 'ц', "ch": 'ч',
    "sh": 'ш', "shch": 'щ', "e": 'е', "yu": 'ю', "ya": 'я'
}

def transliterate(text):
    result = ""
    i = 0
    while i < len(text):
        if i + 3 <= len(text) and text[i:i+3] in translit_map:
            result += translit_map[text[i:i+3]]
            i += 3
        elif i + 2 <= len(text) and text[i:i+2] in translit_map:
            result += translit_map[text[i:i+2]]
            i += 2
        elif text[i] in translit_map:
            result += translit_map[text[i]]
            i += 1
        else:
            result += text[i]
            i += 1
    return result

folder_path = os.path.join(os.getcwd(), 'Schledues')

if os.path.exists(folder_path) and os.path.isdir(folder_path):
    files = os.listdir(folder_path)
    files = [f for f in files if os.path.isfile(os.path.join(folder_path, f)) and f.endswith('.json')]

    for file_name in files:
        file_base = os.path.splitext(file_name)[0]
        file_path = os.path.join(folder_path, file_name)

        transliterated_name = transliterate(file_base)

        try:
            with open(file_path, 'r', encoding='utf-8') as file:
                data = file.read()

            decoded_data = json.loads(data)

            print(f"\nГруппа: {transliterated_name.upper()}")
            print(json.dumps(decoded_data, indent=4, ensure_ascii=False))

        except FileNotFoundError:
            print(f"Ошибка: Файл '{file_name}' не найден.")
        except json.JSONDecodeError:
            print(f"Ошибка: Файл '{file_name}' не является корректным JSON.")
        except Exception as e:
            print(f"Произошла ошибка при обработке '{file_name}': {e}")
else:
    print("Папка Schledues не найдена.")
