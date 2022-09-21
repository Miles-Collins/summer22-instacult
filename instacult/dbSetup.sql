CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- STUB CULTS
CREATE TABLE IF NOT EXISTS cults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  fee INT NOT NULL DEFAULT 0,
  coverImg VARCHAR(255) NOT NULL,
  leaderId VARCHAR(255) NOT NULL,

  FOREIGN KEY (leaderId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

ALTER TABLE cults
ADD COLUMN memberCount INT NOT NULL DEFAULT 0;

INSERT INTO cults
(name, fee, coverImg, leaderId, description)
VALUES
('Give me your SQL', 8900, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSizNxJGboATrqKSFkLJrLL8te-d4GAJXpmXfiHp8xXjG69z5dtY2dQgmMv7_z9C9TUmgQ&usqp=CAU', '631b5b5fa7f0b66bb817725a', 'Develop your future.');

INSERT INTO cults
(name, fee, coverImg, leaderId, description)
VALUES
('No Bootstrap', 0, 'https://colorlib.com/cdn-cgi/image/width=1400,height=802,fit=crop,quality=80,format=auto,onerror=redirect,metadata=none/wp-content/uploads/sites/2/creative-css3-tutorials.jpg', '631b5b5fa7f0b66bb817725a', 'Boostrap is for the weak.  Learn to CSS like a Eldritch God.');

SELECT name, description, id FROM cults;

SELECT
c.name, c.description, c.id,
a.name AS leader
FROM cults c
JOIN accounts a ON a.id = c.leaderId;

  SELECT
            c.*,
            a.*
        FROM cults c
        JOIN accounts a ON a.id = c.leaderId;

-- STUB CULT MEMBERS
CREATE TABLE IF NOT EXISTS cultMembers(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  cultId INT NOT NULL,
  accountId VARCHAR(255),

  FOREIGN KEY (cultId) REFERENCES cults(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';




-- DROP TABLE `cultMembers`;
INSERT INTO `cultMembers`
(cultId, accountId)
VALUES
(1, '6216b36ebc31a249987812b1');

SELECT
c.name,
member.name AS memberName,
leader.name AS leaderName
FROM cultMembers cm
JOIN cults c ON c.id = cm.cultId
JOIN accounts AS member  ON member.id = cm.accountId
JOIN accounts AS leader  ON c.leaderId = leader.id;

SELECT 
  a.*,
  cm.*
FROM cultMembers cm
JOIN accounts a ON a.id = cm.accountId
WHERE cm.cultId = 1;


-- FIXT ME for whatever reason these don't run right
-- CREATE TRIGGER subtractMemberCount 
-- BEFORE DELETE on cultMembers FOR EACH ROW
-- UPDATE cults c SET
--   c.memberCount = c.memberCount - 1
-- WHERE c.id = OLD.cultId;
-- CREATE TRIGGER IF NOT EXISTS addMemberCount 
-- AFTER INSERT on cultMembers FOR EACH ROW
-- UPDATE cults c SET
--   c.memberCount = c.memberCount + 1
-- WHERE c.id = NEW.cultId;