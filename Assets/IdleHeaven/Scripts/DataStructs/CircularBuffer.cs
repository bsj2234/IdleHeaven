using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBuffer
{
    private int _size;
    private int _head;
    private int _tail;
    private int _count;
    private float[] _buffer;

    public CircularBuffer(int size)
    {
        _size = size;
        _head = 0;
        _tail = 0;
        _count = 0;
        _buffer = new float[size];
    }

    public void Enqueue(float item)
    {
        if (_count == _size)
        {
            _head = (_head + 1) % _size;
            _count--;
        }

        _buffer[_tail] = item;
        _tail = (_tail + 1) % _size;
        _count++;
    }

    public float Dequeue()
    {
        if (_count == 0)
        {
            throw new System.InvalidOperationException("CircularBuffer is empty");
        }

        float dequeued = _buffer[_head];
        _head = (_head + 1) % _size;
        _count--;
        return dequeued;
    }

    public void Release()
    {
        _buffer = null;
    }

    public float GetAverage()
    {
        float sum = 0;
        for (int i = 0; i < _count; i++)
        {
            sum += Convert.ToSingle(_buffer[i]);
        }
        return sum / _count;
    }
}
