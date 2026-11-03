<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Evaluator extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'evaluators';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'type',
        'is_built_in',
        'description',
        'created_by_user_id',
        'blocked_at',
        'block_reason',
        'block_message',
    ];

    protected $attributes = [
        'is_built_in' => false,
    ];

    protected $casts = [
        'type' => EvalTemplateType::class,
        'is_built_in' => 'boolean',
        'blocked_at' => 'datetime',
        'block_reason' => EvaluatorBlockReason::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by_user_id');
    }

    public function versions(): HasMany
    {
        return $this->hasMany(EvaluatorVersion::class, 'evaluator_id');
    }

    public function assignments(): HasMany
    {
        return $this->hasMany(EvaluationRuleEvaluatorAssignment::class, 'evaluator_id');
    }
}