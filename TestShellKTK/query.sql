
-- Создание таблицы роли
CREATE TABLE role (
                      id serial PRIMARY KEY,
                      role_name varchar(50) UNIQUE NOT NULL
);

-- Создание таблицы пользователей
CREATE TABLE "user" (
                        id SERIAL PRIMARY KEY,
                        username VARCHAR(50) UNIQUE NOT NULL,
                        password VARCHAR(50) NOT NULL,
                        role INTEGER REFERENCES  role(id) NOT NULL
);

-- Создание таблицы классов
CREATE TABLE class (
                       id SERIAL PRIMARY KEY,
                       name VARCHAR(50) UNIQUE NOT NULL
);

-- Создание таблицы предметов
CREATE TABLE subject (
                         id SERIAL PRIMARY KEY,
                         name VARCHAR(50) UNIQUE NOT NULL,
                         class_id INTEGER REFERENCES class(id)
);

-- Создание таблицы учителей и предметов
CREATE TABLE teacher_subject (
                                 id SERIAL PRIMARY KEY,
                                 teacher_id INTEGER REFERENCES "user"(id),
                                 subject_id INTEGER REFERENCES subject(id)
);

-- Создание таблицы тестов
CREATE TABLE test (
                      id SERIAL PRIMARY KEY,
                      name VARCHAR(100) NOT NULL,
                      description TEXT,
                      created_by INTEGER REFERENCES "user"(id),
                      created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                      subject_id INTEGER REFERENCES subject(id),
                      duration INTERVAL NOT NULL,
                      grading_scale TEXT
);

-- Создание таблицы вопросов
CREATE TABLE question (
                          id SERIAL PRIMARY KEY,
                          test_id INTEGER REFERENCES test(id),
                          question_text TEXT NOT NULL,
                          hint TEXT,
                          weight INTEGER NOT NULL,
                          question_type VARCHAR(50) NOT NULL
);

-- Создание таблицы вариантов ответа
CREATE TABLE answer_option (
                               id SERIAL PRIMARY KEY,
                               question_id INTEGER REFERENCES question(id),
                               answer_text TEXT NOT NULL
);

-- Создание таблицы правильных ответов
CREATE TABLE correct_answer (
                                id SERIAL PRIMARY KEY,
                                question_id INTEGER REFERENCES question(id),
                                correct_option_id INTEGER REFERENCES answer_option(id)
);

-- Создание таблицы назначенных тестов
CREATE TABLE assigned_test (
                               id SERIAL PRIMARY KEY,
                               test_id INTEGER REFERENCES test(id),
                               assigned_by INTEGER REFERENCES "user"(id),
                               assigned_to INTEGER REFERENCES "user"(id),
                               attempts_allowed INTEGER NOT NULL,
                               assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Создание таблицы результатов теста
CREATE TABLE test_result (
                             id SERIAL PRIMARY KEY,
                             assigned_test_id INTEGER REFERENCES assigned_test(id),
                             start_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                             end_time TIMESTAMP,
                             score FLOAT
);