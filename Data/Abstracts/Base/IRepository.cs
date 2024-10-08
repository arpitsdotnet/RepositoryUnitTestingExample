﻿namespace Data.Abstracts.Base;
public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
{
}
