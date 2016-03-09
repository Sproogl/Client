
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <sstream>

#include <winsock2.h>
#include <windows.h>
#include <thread>
#include <string.h>
#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mswsock.lib")





struct WATF
{
	byte type;
	unsigned int ID_SRC;
	unsigned int ID_DEST;
	int MSG_LEN;
	char messange[500];

	WATF()

	{
		type = 111;
		ID_SRC =111;
		ID_DEST = 111;
		MSG_LEN = 111;

	}



};

int hashing(char ip[50])
{
	std::string ID = ip;
	std::hash<std::string> hash_fn;
	unsigned int ID_hash = hash_fn(ID);
	std::cout << ID_hash << '\n';
	return ID_hash;
}




int main(int argc, char* argv[])
{
	SOCKET		s, new_conn , send_sock;
	WSADATA		ws;
	sockaddr_in	sock_bind, new_ca;
	int			new_len, i = 0;
	char		buff[520];
	WATF *mesg;
	// Init 
	bool flag = true;
	std::cout << sizeof(WATF);

	if (FAILED(WSAStartup(0x0002, &ws))) return E_FAIL;

	// Создаём сокет
	if (INVALID_SOCKET == (s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))

		// Выполняем привязку
		ZeroMemory(&sock_bind, sizeof(sock_bind));
	sock_bind.sin_family = AF_INET;
	sock_bind.sin_addr.S_un.S_addr = htonl(INADDR_ANY);		

	sock_bind.sin_port = htons(1332);				// Порт 1234
	std::cout << ("Wait for incoming connections...\n");
	bind(s, (sockaddr*)&sock_bind, sizeof(sock_bind));
	


	// Устанавливаем "слушающий" режим для сокета
	while (1)
	{
	if (FAILED(listen(s, 10)))
	{
		return E_FAIL;
	}
	

		ZeroMemory(&new_ca, sizeof(new_ca));
		new_len = sizeof(new_ca);


				if (FAILED(new_conn = accept(s, (sockaddr*)&new_ca, &new_len)))
				{

					return 0;
				}
		
		
		// Получаем сообщение от клиента
		int msg_len;
		unsigned int hash;
		int radix= 10;
		char *p;

		if (FAILED(msg_len = recv(new_conn, buff, sizeof(struct WATF), 0)))
			return 0;
		//std::cout << "ID DEST : " << wtf.ID_DEST << std::endl << "ID SRC : " << wtf.ID_SRC << std::endl << "MSG LEN :" << wtf.ID_SRC << std::endl << "MESSANGE : " << wtf.messange << std::endl;
		//wtf.type = 101;
		switch (buff[0])
			{
				case 100: //регистрация
					std::cout << "              Registration" <<std:: endl;
					mesg = (WATF*)&buff;
					hash = hashing((char *)&new_ca.sin_addr);
					mesg->ID_SRC = hash;
					send(new_conn, (char*)mesg, sizeof(struct WATF), 0);
		
					break;
				
				 case 101://подключение
					 std::cout << "              Connect" << std::endl;
						break;
						
				 case 102://сообщение
					 std::cout << "              New messange" << std::endl;
					 mesg = (WATF*)&buff;
					 std::cout << "ID_DEST = " << mesg->ID_DEST << std::endl;
					 std::cout << "ID_SRC = " << mesg->ID_SRC << std::endl;
					 std::cout << "message = " << mesg->messange << std::endl;
					 std::cout << "MSG_LEN = " << mesg->MSG_LEN << std::endl;
						 break;
								
				 case 103://отключение
						 break;
				default: break;
			}

	}
	closesocket(new_conn);
	std::cout << ("\n\nDone!");

	return 0;
}
