#include "SQLDB.h"
#include <iostream>


SQLDB::SQLDB()
{
	if (sqlite3_open("usersDB.dblite", &db))
		fprintf(stderr, "Ошибка открытия/создания БД: %s\n", sqlite3_errmsg(db));
	else
	{
		printf_s("DONE");
	}


}


SQLDB::~SQLDB()
{
}

bool SQLDB::login(char *loging, char *password){
	char* SEARCH = "SELECT id FROM user WHERE name = ";
	strcat_s(SEARCH,strlen(loging),loging);
	strcat_s(SEARCH, 2, ";");
	char *err = 0;
	if (sqlite3_exec(db, SEARCH, callbackDB, 0, &err))
	{
		fprintf(stderr, "Ошибка SQL: %sn", err);
		sqlite3_free(err);
	}

	return true;
}

int SQLDB::callbackDB(void *ptr, int argc, char* argv[], char* cols[])
{
	vector<vector<string>> table;
	vector<string> row;
	for (int i = 0; i < argc; i++)
	{
		row.push_back(argv[i] ? argv[i] : "(NULL)");
		cout << argv[i] << " ";
	}
	cout << "\n";
	table.push_back(row);
	return 0;
}