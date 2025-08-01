﻿namespace Shop.Application
{
    public class OperationResult
    {
        public bool IsSuccedded { get; set; }

        public string Message { get; set; }

        public OperationResult()
        {
            IsSuccedded = false;
        }

        public OperationResult Succedded(string message = "Operation Done Successfully")
        {
            IsSuccedded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSuccedded = false;
            Message = message;
            return this;
        }
        
    }
}