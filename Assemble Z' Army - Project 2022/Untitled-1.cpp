

#include <iostream>
​
using std::cout;
using std::cin;
using std::endl;
​
struct Person
{
	unsigned short 	m_age;
	unsigned short 	m_height;
	char			m_name[16];
};
​
​
typedef bool(*CompFunc)(Person*, size_t);
typedef void(*SwapFunc)(Person*, size_t);
​
​
enum ESortingMethod
{
	eByAge,
	eByHeight,
	eByName,
	eNumOfSortingMethods
};
​
​
void int_swap(int* arr, size_t index)
{
	int temp = arr[index];
	arr[index] = arr[index + 1];
	arr[index + 1] = temp;
}
​
​
bool is_successive_smaller(int* arr, size_t index)
{
	return (arr[index] > arr[index + 1]);
}
​
​
void bubble_sort(int* arr, size_t size)
{
	for (size_t i = 0; i < size - 1; ++i)
	{
		for (size_t j = 0; j < size - i - 1; ++j)
		{
			if (is_successive_smaller(arr, j))
			{
				int_swap(arr, j);
			}
		}
	}
}
​
​
void print_person(const Person& person)
{
	cout << person.m_name << " is " << person.m_age << " years old, with " << person.m_height << " height" << endl;
}
​
​
void print_array(int* arr, size_t size)
{
	for (size_t i = 0; i < size; ++i)
	{
		cout << arr[i] << " ";
	}
​
	cout << endl;
}
​
​
void print_people(Person* people, size_t size, const char* title)
{
	cout << title << endl << "-------" << endl;
​
	for (size_t i = 0; i < size; ++i)
	{
		print_person(people[i]);
	}
}
​
​
bool comp_by_age(Person* people, size_t index)
{
	return (people[index].m_age > people[index + 1].m_age);
}
​
​
bool comp_by_height(Person* people, size_t index)
{
	return (people[index].m_height > people[index + 1].m_height);
}
​
​
#include <cstring> // used for memcpy. Later on we'll implement that by ourselves.
​
void person_swap(Person* people, size_t index)
{
	Person temp(people[index]);
​
	memcpy(&people[index], &people[index + 1], sizeof(Person));
	memcpy(&people[index + 1], &temp, sizeof(Person));
}
​
​
void person_sort(Person* people, size_t size, CompFunc gComp, SwapFunc gSwap)
{
	for (size_t i = 0; i < size - 1; ++i)
	{
		for (size_t j = 0; j < size - i - 1; ++j)
		{
			if (gComp(people, j))
			{
				gSwap(people, j);
			}
		}
	}
}
​
void person_sort_ugly(Person* people, size_t size, ESortingMethod eMethod)
{
	for (size_t i = 0; i < size - 1; ++i)
	{
		for (size_t j = 0; j < size - i - 1; ++j)
		{
			if (eMethod == eByAge)
			{
				if (comp_by_age(people, j))
				{
					person_swap(people, j);
				}
			}
			else if (eMethod == eByHeight)
			{
				if (comp_by_height(people, j))
				{
					person_swap(people, j);
				}
			}
		}
	}
}
​
​
int main()
{
	cout << "----------- int bubble sort test -----------" << endl;
​
	int arr[] = { 3, 0, 9, 2, -1, 22, 23, 11, 4 };
	print_array(arr, sizeof(arr) / sizeof(int));
	bubble_sort(arr, sizeof(arr) / sizeof(int));
	print_array(arr, sizeof(arr) / sizeof(int));
	
	cout << "----------- person sort test -----------" << endl;
​
	Person people[3] = { { 31, 173, "Shabi" }, { 19, 168, "Uza" }, { 44, 163, "Batz" } };
​
	print_people(people, sizeof(people) / sizeof(Person), "Before:");
	person_sort(people, sizeof(people) / sizeof(Person), comp_by_height, person_swap);
	print_people(people, sizeof(people) / sizeof(Person), "After:");
​
	return 0;
}