<?php

namespace App\Models;

enum EvaluatorSourceCodeLanguage: string
{
    case PYTHON = 'PYTHON';
    case TYPESCRIPT = 'TYPESCRIPT';
}