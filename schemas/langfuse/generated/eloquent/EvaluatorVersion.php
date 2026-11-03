<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EvaluatorVersion extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'evaluator_versions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'evaluator_id',
        'version',
        'created_by_user_id',
        'prompt',
        'prompt_messages',
        'partner',
        'model',
        'provider',
        'model_params',
        'vars',
        'variable_mapping',
        'output_definition',
        'source_code',
        'source_code_language',
        'questions',
    ];

    protected $casts = [
        'version' => 'integer',
        'prompt_messages' => 'array',
        'model_params' => 'array',
        'variable_mapping' => 'array',
        'output_definition' => 'array',
        'source_code_language' => EvaluatorSourceCodeLanguage::class,
        'questions' => 'array',
    ];

    public function evaluator(): BelongsTo
    {
        return $this->belongsTo(Evaluator::class, 'evaluator_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by_user_id');
    }
}