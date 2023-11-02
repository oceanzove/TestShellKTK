



-- Создание таблицы классов
CREATE TABLE class (
                       id SERIAL PRIMARY KEY,
                       name VARCHAR(60) UNIQUE NOT NULL
);


-- Создание таблицы пользователей
CREATE TABLE student (
                         id SERIAL PRIMARY KEY,
                         username VARCHAR(60) UNIQUE NOT NULL,
                         password VARCHAR(60) NOT NULL,
                         class_id INTEGER REFERENCES class(id)
);


-- Создание таблицы предметов
CREATE TABLE subject (
                         id SERIAL PRIMARY KEY,
                         name VARCHAR(50) UNIQUE NOT NULL
);

-- Создание таблицы связывающей класс и предмет
CREATE TABLE class_subject (
                               id SERIAL PRIMARY KEY,
                               subject_id INTEGER REFERENCES subject(id)
);

-- Создание таблицы учителей
CREATE TABLE teacher (
                         id SERIAL PRIMARY KEY,
                         username varchar(60) NOT NULL ,
                         password varchar(60) NOT NULL
);

-- Создание таблицы учителей и предметов
CREATE TABLE teacher_subject (
                                 id SERIAL PRIMARY KEY,
                                 teacher_id INTEGER REFERENCES teacher(id),
                                 subject_id INTEGER  REFERENCES subject(id)
);

-- Создание таблицы тестов
CREATE TABLE test (
                      id SERIAL PRIMARY KEY,
                      name VARCHAR(100) NOT NULL,
                      description TEXT,
                      created_by INTEGER REFERENCES teacher(id),
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

CREATE TABLE answer_options (
                                id SERIAL PRIMARY KEY,
                                question_id INTEGER REFERENCES question(id),
                                answer_text TEXT NOT NULL
);

CREATE TABLE correct_answers (
                                 id SERIAL PRIMARY KEY,
                                 question_id INTEGER REFERENCES question(id),
                                 correct_option_id INTEGER REFERENCES answer_options(id)
);

-- Создание таблицы назначенных тестов
CREATE TABLE assigned_tests (
                                id SERIAL PRIMARY KEY,
                                test_id INTEGER REFERENCES test(id),
                                assigned_by INTEGER REFERENCES student(id),
                                assigned_to INTEGER REFERENCES student(id),
                                attempts_allowed INTEGER NOT NULL,
                                assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Создание таблицы результатов теста
CREATE TABLE test_results (
                              id SERIAL PRIMARY KEY,
                              assigned_test_id INTEGER REFERENCES assigned_tests(id),
                              start_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                              end_time TIMESTAMP,
                              score FLOAT
);

