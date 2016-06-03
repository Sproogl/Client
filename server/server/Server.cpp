#include "Server.h"
#include <thread>
#include <mutex>


std::mutex g_lock;

Server::Server()
{

	if (FAILED(WSAStartup(0x0002, &ws))) return;

	// Создаём сокет
	if (INVALID_SOCKET == (s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))

		// Выполняем привязку
		ZeroMemory(&sock_bind, sizeof(sock_bind));
	sock_bind.sin_family = AF_INET;
	sock_bind.sin_addr.S_un.S_addr = htonl(INADDR_ANY);

	sock_bind.sin_port = htons(1332);				// Порт 1234
	std::cout << ("Wait for incoming connections...\n");
	bind(s, (sockaddr*)&sock_bind, sizeof(sock_bind));
	


}


Server::~Server()
{
}


void Server::createMesg(WATF mesg1, SOCKET new_connT){
	WATF *mesg=new WATF();
	*mesg = mesg1;
	
	switch (mesg->type)
	{
	case 100: //регистрация
		std::cout << "              Registration" << std::endl;
		
		
		mesg->ID_SRC = 100;
		send(new_connT, (char*)mesg, sizeof(struct WATF), 0);
		

		break;

	case 101://подключение
	{
	
	
		std::cout << "              Connect" << std::endl;
		auto it = ClientList.find(mesg->ID_DEST);
		if (it == ClientList.end())
		{
			ClientList.insert(std::pair<unsigned int, SOCKET>(mesg->ID_SRC, new_connT));
		}
		else
		{
			ClientList.erase(it);

		}
		
		break;
	}

	case 102://сообщение
	{
		std::cout << "              New messange" << std::endl;
		
		std::cout << "ID_DEST = " << mesg->ID_DEST << std::endl;
		std::cout << "ID_SRC = " << mesg->ID_SRC << std::endl;
		std::cout << "message = " << mesg->messange << std::endl;
		std::cout << "MSG_LEN = " << mesg->MSG_LEN << std::endl;
		auto it = ClientList.find(mesg->ID_DEST);
		if (it == ClientList.end())
		{
			mesg->type = 104;
			std::cout << "ERROR";
			send(new_connT, (char*)mesg, sizeof(struct WATF), 0);
		}
		else
		{
			std::cout << "DONE";
			send(it->second, (char*)mesg, sizeof(struct WATF), 0);

		}
		closesocket(new_connT);
		
		break;
	}

	case 103://отключение
	{
		std::cout << "              Disconnect" << std::endl;
		
		
		std::cout << "ID_DEST = " << mesg->ID_DEST << std::endl;
		std::cout << "ID_SRC = " << mesg->ID_SRC << std::endl;
		std::cout << "message = " << mesg->messange << std::endl;
		std::cout << "MSG_LEN = " << mesg->MSG_LEN << std::endl;
		auto it = ClientList.find(mesg->ID_SRC);
		if (it == ClientList.end())
		{
			closesocket(new_connT);
		}
		else
		{
			mesg->type = 103;
			send(it->second, (char*)mesg, sizeof(struct WATF), 0);
			ClientList.erase(it);
			closesocket(new_connT);

		}
		
		break;

	}
	case 105:				// online of offline user
	{
		std::cout << "              Status" << std::endl;
		
		
		auto it = ClientList.find(mesg->ID_DEST);
		auto itSRC = ClientList.find(mesg->ID_SRC);
		if (it == ClientList.end())
		{
			std::cout << "              OFFLINE - " << mesg->ID_DEST << std::endl;
			closesocket(new_connT);
		}
		else
		{
			std::cout << "              ONLINE - " << mesg->ID_DEST << std::endl;

			send(itSRC->second, (char*)mesg, sizeof(struct WATF), 0);
			mesg->ID_DEST = mesg->ID_SRC;
			send(it->second, (char*)mesg, sizeof(struct WATF), 0);
		}
		
	}
	default: break;
	}
}


bool Server::startServer(){
	// Устанавливаем "слушающий" режим для сокета
	WATF mesg;
	while (1)
	{
		if ((listen(s, 10)))
		{
			return false;
		}


		ZeroMemory(&new_ca, sizeof(new_ca));
		new_len = sizeof(new_ca);


		if (FAILED(new_conn = accept(s, (sockaddr*)&new_ca, &new_len)))
		{

			continue;
		}


		// Получаем сообщение от клиента
		int msg_len;
		unsigned int hash;
		int radix = 10;

		if (FAILED(msg_len = recv(new_conn, buff, sizeof(struct WATF), 0)))
			continue;
		//std::cout << "ID DEST : " << wtf.ID_DEST << std::endl << "ID SRC : " << wtf.ID_SRC << std::endl << "MSG LEN :" << wtf.ID_SRC << std::endl << "MESSANGE : " << wtf.messange << std::endl;
		//wtf.type = 101;
		mesg = *(WATF*)&buff;
		createMesg(mesg,new_conn);
		

	}
	closesocket(new_conn);
	return false;


}

int Server::hashing(char ip[50])
{
	std::string ID = ip;
	std::hash<std::string> hash_fn;
	unsigned int ID_hash = hash_fn(ID);
	std::cout << ID_hash << '\n';
	return ID_hash;
}