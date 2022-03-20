namespace Shared.DataTransferObjects;

public record PersonForUpdateDto(string FirstName, string LastName, 
    GenderTypeDto Gender, string PersonalNumber, DateTime BirthDate, string City,
    IEnumerable<PhoneNumberDto> PhoneNumbers);