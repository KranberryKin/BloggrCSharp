CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

SELECT * FROM accounts;

CREATE TABLE IF NOT EXISTS blogs(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
  title VARCHAR(80) COMMENT 'Blog Title',
  body VARCHAR(5000) COMMENT 'Blog Body',
  imgUrl VARCHAR(255) COMMENT 'Blog Img Link',
  published TINYINT DEFAULT 0 COMMENT 'Default False',
  creatorId VARCHAR(255) COMMENT 'Creator'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'Primary Key',
creatorId VARCHAR(255) COMMENT 'Creator',
body VARCHAR(255) COMMENT 'Comments Body',
blog int COMMENT 'Set Id when comment on specific Blog.'
) default charset utf8 COMMENT '';
