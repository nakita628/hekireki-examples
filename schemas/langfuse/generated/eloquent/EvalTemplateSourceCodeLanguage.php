<?php

namespace App\Models;

enum EvalTemplateSourceCodeLanguage: string
{
    case PYTHON = 'PYTHON';
    case TYPESCRIPT = 'TYPESCRIPT';
}