﻿using GraphQL.Types;
using Library.API.Services;
using System;

namespace Library.API.GraphQLSchema
{
    public class LibraryQuery : ObjectGraphType
    {
        public LibraryQuery(IRepositoryWrapper repositoryWrapper)
        {
            Field<ListGraphType<AuthorType>>("authors", resolve: context =>
                repositoryWrapper.Author.GetAllAsync().Result);

            Field<AuthorType>("author", arguments: new QueryArguments(new QueryArgument<IdGraphType>()
            {
                Name = "id",
            }),
                resolve: context =>
                {
                    Guid id = Guid.Empty;
                    if (context.Arguments.ContainsKey("id"))
                    {
                        id = new Guid(context.Arguments["id"].ToString());
                    }

                    return repositoryWrapper.Author.GetByIdAsync(id).Result;
                });
        }
    }
}