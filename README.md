## Установка и настройка

1. Скачайте проект и откройте папку с проектом.
2. Зайдите в свою PostgreSQL базу данных и запустите скрипт `create_table.sql`.
3. Найдите файл `Appconf.json` по пути `Pract_final-master\Pract_final\bin\Debug`, откройте его и внесите изменения для подключения программы к своей базе данных.
4. При желании найдите файл `pgslog.log` и добавьте свои логи. (Логи с сайта "https://www.ossec.net/docs/log_samples/apache/apache.html" уже занесены в файл).

## Запуск программы

1. Для запуска найдите файл `Pract_final.exe` по пути `Pract_final-master\Pract_final\bin\Debug` и откройте его.

## Работа программы

1. Для работы программы необходимо зарегистрироваться. Нажмите кнопку "Зарегистрироваться" (для последующих использований можно использовать вход под своим логином и паролем).
   Примечание: После регистрации/входа рекомендуется нажать кнопку "Очистить данные" для корректной работы программы.
2. Занесите данные с помощью кнопки "Добавить данные".
3. После этого можно использовать следующие функции:
   - Вывод всех данных;
   - Группировка по IP и дате;
   - Выборка по дате (для использования выборки по дате, укажите начальную и конечную дату в соответствующих формах).
4. Также вы можете очистить данные, выйти из своего аккаунта и выйти из программы.
5. Для сохранения данных нажмите кнопку "Сохранить в формате .json". Затем выберите нужное вам сохранение в новом окне (можно сохранять несколько вариантов). Кнопка "Вернуться на главную" вернет вас на главную страницу. После сохранения данные можно найти по пути `Pract_final-master\Pract_final\bin\Debug` в файлах: `all_table.json`, `group_ip.json`, `group_date.json`, `selection_date.json`.
6. Для выхода используйте кнопку "Выйти из программы".
