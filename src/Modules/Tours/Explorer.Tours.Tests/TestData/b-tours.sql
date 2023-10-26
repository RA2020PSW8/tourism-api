INSERT INTO tours."Tours"(
    "Id", "UserId", "Name", "Description", "Price", "Difficulty", "TransportType", "Status", "Tags")
VALUES (-1, 1, 'TestTour', 'Wow, epic', 1234, 1, 1, 1, ARRAY[('Adventure','Epic')]);
INSERT INTO tours."Tours"(
    "Id", "UserId", "Name", "Description", "Price", "Difficulty", "TransportType", "Status", "Tags")
VALUES (-2, 1, 'TestTour2', 'ok', 12000, 1, 1, 1, ARRAY[('cringe','kinda')]);
INSERT INTO tours."Tours"(
    "Id", "UserId", "Name", "Description", "Price", "Difficulty", "TransportType", "Status", "Tags")
VALUES (-3, 1, 'TestTour3', 'not ok', 12342, 1, 1, 1, ARRAY[('Wow','fable')]);