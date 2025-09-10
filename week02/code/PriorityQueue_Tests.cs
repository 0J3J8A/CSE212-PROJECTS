using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with different priorities and call Dequeue
    // Expected Result: Dequeue returns the item with the highest priority
    // Defect(s) Found: Original Dequeue loop skipped the last element. Also item wasn't removed from queue.
    public void TestPriorityQueue_ReturnsHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Enqueue items with the same highest priority and ensure FIFO is respected
    // Expected Result: The first item enqueued among those with highest priority is dequeued first
    // Defect(s) Found: None after fix
    public void TestPriorityQueue_FIFOSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("X", 5);
        priorityQueue.Enqueue("Y", 5);
        priorityQueue.Enqueue("Z", 4);

        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();

        Assert.AreEqual("X", result1); // X and Y have same priority; X first
        Assert.AreEqual("Y", result2); // Then Y
    }

    [TestMethod]
    // Scenario: Enqueue multiple items and dequeue all, checking order by priority
    // Expected Result: Items come out in priority order, using FIFO for ties
    // Defect(s) Found: None after fix
    public void TestPriorityQueue_DequeueAllItems()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("One", 2);
        priorityQueue.Enqueue("Two", 4);
        priorityQueue.Enqueue("Three", 4);
        priorityQueue.Enqueue("Four", 1);

        Assert.AreEqual("Two", priorityQueue.Dequeue());
        Assert.AreEqual("Three", priorityQueue.Dequeue());
        Assert.AreEqual("One", priorityQueue.Dequeue());
        Assert.AreEqual("Four", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Interleave Enqueue and Dequeue
    // Expected Result: Queue maintains correct order and priority behavior
    // Defect(s) Found: None after fix
    public void TestPriorityQueue_InterleaveOperations()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Alpha", 1);
        priorityQueue.Enqueue("Beta", 3);
        Assert.AreEqual("Beta", priorityQueue.Dequeue());

        priorityQueue.Enqueue("Gamma", 2);
        priorityQueue.Enqueue("Delta", 5);
        Assert.AreEqual("Delta", priorityQueue.Dequeue());
        Assert.AreEqual("Gamma", priorityQueue.Dequeue());
        Assert.AreEqual("Alpha", priorityQueue.Dequeue());
    }
}
