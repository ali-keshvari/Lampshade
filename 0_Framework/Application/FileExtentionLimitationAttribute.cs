﻿using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application;

public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string[] _validExtentions;

    public FileExtentionLimitationAttribute(string[] validExtentions)
    {
        _validExtentions = validExtentions;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        //context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-fileExtenstionLimit", ErrorMessage);
    }

    public override bool IsValid(object value)
    {
        var file = value as IFormFile;
        if (file == null) return true;
        var fileExtention = Path.GetExtension(file.FileName);
        return _validExtentions.Contains(fileExtention);
    }
}