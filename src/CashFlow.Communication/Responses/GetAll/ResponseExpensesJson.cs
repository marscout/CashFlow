﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Communication.Responses.GetAll
{
    public class ResponseExpensesJson
    {
        public List<ResponseShortExpenseJson> Expenses { get; set; } = [];
    }
}
