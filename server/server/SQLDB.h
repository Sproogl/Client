#pragma once
#pragma comment (lib, "sqlite3.lib")
#include "vector"
#include "sqlite3.h"


using namespace std;
class SQLDB
{
private:
	sqlite3 *db; 

public:
	SQLDB();
	~SQLDB();
	bool login(char *loging, char *password);
	vector<unsigned int> getFriendsFromDB(unsigned int);
	bool insertFriendsToDB(unsigned int,vector<unsigned int>);
	static int callbackDB(void *ptr, int argc, char* argv[], char* cols[]);
};

