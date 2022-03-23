/*#define _CRT_SECURE_NO_WARNINGS
#include<iostream>
#include<algorithm>
#include<vector>


class Human {
private:
	std::string Name;
	int Age;
public: Human(std::string IName, int iAge) :Name(IName), Age(iAge) {}
};
class MStr {
private:
	char* Buffer;
public:
	MStr(const char* Input) {
		if (Input != NULL) {
			Buffer = new char[strlen(Input) + 1];
			strcpy(Buffer, Input);
		}
		else Buffer = NULL;
	}
	MStr(const MStr& Input) {
		if (Input.Buffer != NULL) {
			Buffer = new char[strlen(Input.Buffer) + 1];
			strcpy(Buffer,Input.Buffer);
		}
	}
	~MStr() {
		std::cout << "destructor" << std::endl;
		if (Buffer != NULL)
			delete[] Buffer;
	}
	int GetLength() {
		return strlen(Buffer);
	}
	const char* GetString() {
		return Buffer;
	}
};

void DisplayString(MStr& Input) {
	std::cout << Input.GetString() << std::endl;
}
void DisplayString(MStr Input) {
	std::cout << "Buffer " << Input.GetString() << std::endl;
}

void DisplayNums(std::vector<int>& DynArray) {
	for_each(DynArray.begin(), DynArray.end(), [](int Element) {
		std::cout << Element << " ";
		});
	std::cout << std::endl;
}

void main() {
	std::vector<int> nums;
	nums.push_back(501);
	nums.push_back(-1);
	nums.push_back(25);
	nums.push_back(-35);
	DisplayNums(nums);
	std::sort(nums.begin(), nums.end(), [](int n1, int n2) {
		return (n2 < n1);
	});
	DisplayNums(nums);
	float Age = 30;
	float* pInt = &Age;
	int const a = 5;
	int* p = (int*)&a;
	*p = 10;
	int* pNums = new(std::nothrow) int[5];
	std::cout << *p << std::endl;
	MStr Hello("HELLO");
	DisplayString(Hello);
}
*/
#include<iostream>
#include <vector>
#include <string>

class Buffer
{
public:
	Buffer(const std::string& buff)
		: pBuff(nullptr)
		, buffSize(buff.length())
	{
		pBuff = new char[buffSize];
		memcpy(pBuff, buff.c_str(), buffSize);
	}

	~Buffer() { destroy(); }

	Buffer(const Buffer& other)
		: pBuff(nullptr)
		, buffSize(other.buffSize)
	{
		pBuff = new char[buffSize];
		memcpy(pBuff, other.pBuff, buffSize);
	}

	Buffer& operator=(const Buffer& other)
	{
		destroy();
		buffSize = other.buffSize;
		pBuff = new char[buffSize];
		memcpy(pBuff, other.pBuff, buffSize);
		return *this;
	}

	Buffer(Buffer&& tmp)
		: pBuff(tmp.pBuff)
		, buffSize(tmp.buffSize)
	{
		tmp.pBuff = nullptr;
	}

	Buffer& operator=(Buffer&& tmp)
	{
		destroy();
		buffSize = tmp.buffSize;
		pBuff = tmp.pBuff;
		tmp.pBuff = nullptr;
		return *this;
	}

private:
	void destroy()
	{
		if (pBuff)
			delete[] pBuff;
	}

	char* pBuff;
	size_t buffSize;
};

Buffer CreateBuffer(const std::string& buff)
{
	Buffer retBuff(buff);
	return retBuff;
}

class Singleton {
private:
	static Singleton* instance;
private:
	Singleton() = default;
	~Singleton() = default;
	Singleton(const Singleton&) = delete;
	Singleton& operator=(const Singleton&) = delete;
public:
	static Singleton* getInstance() {
		if (instance == nullptr) {
			instance = new Singleton();
		}
		return instance;
	}
	static Singleton& getInstanceRef() {
		static Singleton instanceRef;
		// volatile int dummy{};
		return instanceRef;
	}
};
Singleton* Singleton::instance = 0;
Buffer* bf1 = new Buffer("111");

template<typename T> class Node {
public:
	T* Data;
	Node<T>* Next;
	Node(T& data) :Data(&data) {}
};
template<typename T> class LinkedList {
	Node<T>* head=nullptr; // головной/первый элемент
	Node<T>* tail=nullptr; // последний/хвостовой элемент
	int count = 0;  // количество элементов в списке
public:
	void appendFirst(T& data) {
		Node<T>* node = new Node<T>(*data);
		Node<T>* temp = head;
		head = node;
		head->Next = temp;
	}
	void append(T& data) {
		count++;
		Node<T>* node = new Node<T>(data);
		if (head == nullptr && tail == nullptr) {
			head = node;
			tail = node;
			return;
		}
		tail->Next = node;
		tail = node;
	}
};

int main()
{
	LinkedList<int> list;
	int* p = new int;
	*p = 5;
	list.append(*p);
	int* p1 = new int;
	*p1 = 10;
	list.append(*p1);

	Singleton* instance = Singleton::getInstance();
	//Singleton& instanceRef = Singleton::getInstanceRef();
	//std::cout << instance << std::endl;
	//std::cout << &instanceRef << std::endl;
	Buffer buffer1 = CreateBuffer("123"); // срабатывает конструктор перемещения
	Buffer buffer2 = buffer1;             // срабатывает конструктор копирования
	buffer2 = CreateBuffer("123");        // срабатывает конструктор перемещения, затем оператор перемещения
	buffer2 = buffer1;                    // срабатывает оператор присваивания
}
