using UnityEngine;
using System.Collections;


/* Priority queue for A* implementation */
public class PriorityQueue<T> where T : System.IComparable {

	/* Note index 1 is the front of the heap */
	public T[] minHeap;
	public int size;
	public int maxsize; 

	/* constructs new min heap */
	public PriorityQueue (int size) {
		maxsize = size;
		this.size = 0;
		minHeap = new T[maxsize + 1];
		minHeap [0] = default(T);
	}

	private int getParent(int pos) {
		return pos / 2;
	}

	private int getLeftChild(int pos) {
		return 2 * pos;
	}

	private int getRightChild(int pos) {
		return 2 * pos + 1;
	}

	private bool isLeaf(int pos) {
		return (pos >= size/2 && pos <= size);
	}

	private void swap (int fst, int snd) {
		T tmp = minHeap[fst];
		minHeap [fst] = minHeap [snd];
		minHeap [snd] = tmp; 
	}

	private void Heapify (int pos) {
		if (!isLeaf(pos)) {
			if (minHeap[pos].CompareTo(minHeap[getLeftChild(pos)]) > 0  || minHeap[pos].CompareTo(minHeap[getRightChild(pos)]) > 0) {
				if (minHeap[getLeftChild(pos)].CompareTo(minHeap[getRightChild(pos)]) < 0) {
					swap(getLeftChild(pos), pos);
					Heapify(getLeftChild(pos));
				}
				else {
					swap(getRightChild(pos), pos);
					Heapify(getRightChild(pos));
				}
			}
		}
	}
				           
	public void queue(T element) {
		if (size == maxsize)
			return;
		minHeap [++size] = element;
		int pos = size;
		while (minHeap[pos].CompareTo(minHeap[getParent(pos)]) < 0 || pos == 1) {
			swap (pos, getParent(pos));
			pos = getParent(pos);
		}
	}

	public T dequeue() {
		T removed = minHeap [size];
		minHeap[1] = minHeap[size--];
		Heapify (1);
		return removed; 
	}
}
