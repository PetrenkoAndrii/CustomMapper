using Mapper.Dto;
using Mapper.Models;
using Mapper.ReflectionMapper;

Console.WriteLine("Hello, Mapper!");

var user = new User
{ 
    Name = "Bill", 
    Age = 20,
    Equipment = new()
    {
        Id = Guid.NewGuid(),
        Number = 123,
        Name = "Screwdriver"
    }
};

var reflectionMapper = new ReflectionMapper_V1();
var userDto = reflectionMapper.Map<User, UserDto>(user);
if (userDto != null && userDto.Name == user.Name && userDto.Age == user.Age && userDto.Equipment == null)
{
    Console.WriteLine("\n1. Simple Mapping successful!");
}

var extendedReflectionMapper = new ReflectionMapper_V2();
var userDtoWithInternalObject = extendedReflectionMapper.Map<User, UserDto>(user);

if (userDtoWithInternalObject != null && userDtoWithInternalObject.Name == user.Name && userDtoWithInternalObject.Age == user.Age &&
    userDtoWithInternalObject.Equipment != null && userDtoWithInternalObject.Equipment.Name == user.Equipment.Name)
{
    Console.WriteLine("\n2. Extended Mapping successful!");
}
