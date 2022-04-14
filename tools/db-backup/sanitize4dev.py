import sys


table_names = [
    'Activities',
    'Billabilities',
    'EditLocks',
    'FavouriteActivities',
    'RecordEntries',
    'Records',
    'Subprojects',
    'UserBalanceCorrections',
    'UserPartTimeSettings',
    'Users',
    '__EFMigrationsHistory',
]


def _sanitize_table_names(line):
    for name in table_names:
        new = f'`{name}`'
        old = new.lower()
        line = line.replace(old, new)

    return line


def _sanitize_content(in_file, out_file):
    for line in in_file.readlines():
        line = _sanitize_table_names(line)
        out_file.write(line)


def sanitize_for_dev(in_path, out_path):
    with open(in_path, 'r', encoding='utf-8') as in_file:
        with open(out_path, 'w', encoding='utf-8', newline='\n') as out_file:
            _sanitize_content(in_file, out_file)


if __name__ == '__main__':
    sanitize_for_dev(sys.argv[1], sys.argv[2])
