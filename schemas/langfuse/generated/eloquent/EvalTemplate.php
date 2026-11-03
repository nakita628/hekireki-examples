<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EvalTemplate extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'eval_templates';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'version',
        'prompt',
        'type',
        'partner',
        'model',
        'provider',
        'model_params',
        'vars',
        'output_schema',
        'source_code',
        'source_code_language',
    ];

    protected $attributes = [
        'type' => 'LLM_AS_JUDGE',
    ];

    protected $casts = [
        'version' => 'integer',
        'type' => EvalTemplateType::class,
        'model_params' => 'array',
        'output_schema' => 'array',
        'source_code_language' => EvalTemplateSourceCodeLanguage::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function JobConfiguration(): HasMany
    {
        return $this->hasMany(JobConfiguration::class, 'eval_template_id');
    }
}