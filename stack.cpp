#include<iostream>
#include<vector>
#include<cstdlib>
#include<string>
#include<stdexcept>

using namespace std;

template <class T> class Stack {
	private:vector<T> elements;
	public:
		void push(T const&);
		void pop();
		T top();
		bool empty();
};

template <class T>
void Stack<T>::push(T const& elem) {
	elements.push_back(elem);
}
template <class T>
void Stack<T>::pop() {
	if (elements.empty())
		throw out_of_range("Stack<T>::pop:empty stack");
	else elements.pop_back();
}
template <class T> 
bool Stack<T>::empty() {
	return elements.empty();
}
template <class T> T Stack<T>::top() {
	if (empty()) {
		throw out_of_range("Stack<T>::top:empty stack");
	}
	return elements.back();
}

template<typename T>
class Foo {
	Foo(const T&);

	void func1()const;
	void func2(const Foo&)const;
	Foo func3() const;
	Foo func4() const;
private:
	T m_foo;
};


//��������� ����������� ��� ��� T
template <typename T>
//����������� ���� T
Foo<T>::Foo(const T& foo):m_foo(foo) {

}

template <typename T>
void Foo<T>::func1() const {
	std::cout << "func1:" << m_foo << std::endl;
}



int main() {
	try {
		Stack<int> iStack;
		Stack<string> sStack;
		iStack.push(7);
		cout << iStack.top() << endl;

		sStack.push("HELLO");
		cout << sStack.top() << std::endl;
		sStack.pop();
		sStack.pop();

	}
	catch (exception const &ex) {
		cerr << "Exception: " << ex.what() << endl;
	}
	return 0;
}
