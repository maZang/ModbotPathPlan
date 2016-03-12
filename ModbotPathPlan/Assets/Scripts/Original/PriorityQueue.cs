using UnityEngine;
using System.Collections;

namespace Original {
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
				int leftIndex = getLeftChild(pos);
				int rightIndex = getRightChild(pos);
				if ((size >= leftIndex && minHeap[pos].CompareTo(minHeap[leftIndex]) > 0)  || 
				(size >= rightIndex && minHeap[pos].CompareTo(minHeap[rightIndex]) > 0)) {
					if (minHeap[leftIndex].CompareTo(minHeap[rightIndex]) < 0) {
						swap(leftIndex, pos);
						Heapify(leftIndex);
					}
					else {
						swap(rightIndex, pos);
						Heapify(rightIndex);
					}
				}
			}
		}
					           
		public void queue(T element) {
			if (size == maxsize)
				return;
			minHeap [++size] = element;
			int pos = size;
			while (pos > 1 && minHeap[pos].CompareTo(minHeap[getParent(pos)]) < 0) {
				swap (pos, getParent(pos));
				pos = getParent(pos);
			}
		}

		public T dequeue() {
			T removed = minHeap [1];
			minHeap[1] = minHeap[size--];
			Heapify (1);
			return removed; 
		}

		public int getSize() {
			return size;
		}
	}
}
