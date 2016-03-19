using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> {

	private class QueueObject {
		public T data;
		public float priority;

		public QueueObject(T data, float priority) {
			this.data = data;
			this.priority = priority;
		}
	}

	private QueueObject[] minHeap;
	private int size;
	private int maxsize;

	public PriorityQueue (int size) {
		maxsize = size;
		this.size = 0;
		minHeap = new QueueObject[maxsize + 1];
		minHeap [0] = null;
	}

	private int getParent(int pos) {
		return pos/2;
	}

	private int getLeftChild(int pos) {
		return 2 * pos;
	}

	private int getRightChild(int pos) {
		return 2*pos + 1;
	}

	private bool isLeaf(int pos) {
		return (pos >= size/2 && pos <= size);
	}

	private void swap (int fst, int snd) {
		QueueObject tmp = minHeap[fst];
		minHeap[fst] = minHeap[snd];
		minHeap[snd] = tmp;
	}

	private void Heapify (int pos) {
		if (!isLeaf(pos)) {
			int leftIndex = getLeftChild(pos);
			int rightIndex = getRightChild(pos);
			if ((size >= leftIndex && minHeap[pos].priority > minHeap[leftIndex].priority) ||
				(size >= rightIndex && minHeap[pos].priority > minHeap[rightIndex].priority)) {
				swap(leftIndex, pos);
				Heapify(leftIndex);
			} else {
				swap(rightIndex, pos);
				Heapify(rightIndex);
			}
		}
	}

	public void queue(float priority, T element) {
		if (size == maxsize) {
			return;
		}
		QueueObject t = new QueueObject(element, priority);
		minHeap[++size] = t;
		int pos = size;
		while (pos > 1 && minHeap[pos].priority - minHeap[getParent(pos)].priority < 0) {
			swap (pos, getParent(pos));
			pos = getParent(pos);
		}
	}

	public T dequeue() {
		QueueObject removed = minHeap[1];
		minHeap[1] = minHeap[size--];
		Heapify(1);
		return removed.data;
	}

	public bool Contains(float priority, T elem) {
		return true;
	}

	public int getSize() {
		return size;
	}
}
