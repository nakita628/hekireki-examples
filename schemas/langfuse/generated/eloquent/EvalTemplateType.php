<?php

namespace App\Models;

enum EvalTemplateType: string
{
    case LLM_AS_JUDGE = 'LLM_AS_JUDGE';
    case CODE = 'CODE';
    case DECISION_MODEL = 'DECISION_MODEL';
    case FACET = 'FACET';
}