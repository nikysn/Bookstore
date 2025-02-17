﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;  // тут работает инверсия зависимостей
        public BookService(IBookRepository bookRepository) // патерн конструктор инжекшен - внедрение через конструктор
        {
            this._bookRepository = bookRepository;
        }
        public Book[] GetAllByQuery(string query)   // этот метод должен вернуть массив книг  по : 
        {
            if(Book.IsIsbn(query))
                return _bookRepository.GetAllByIsbn(query); // Isbn 

            return _bookRepository.GetAllByTitleOrAuthor(query); // или по Автору
        }
    }
}
