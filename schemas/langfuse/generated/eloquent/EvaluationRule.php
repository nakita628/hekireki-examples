<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EvaluationRule extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'evaluation_rules';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'created_by_user_id',
        'name',
        'status',
        'target_object',
        'filter',
        'sampling',
        'delay',
        'idle_time',
        'time_scope',
    ];

    protected $attributes = [
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'status' => JobConfigState::class,
        'filter' => 'array',
        'sampling' => AsDecimal::class,
        'delay' => 'integer',
        'idle_time' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by_user_id');
    }

    public function assignments(): HasMany
    {
        return $this->hasMany(EvaluationRuleEvaluatorAssignment::class, 'evaluation_rule_id');
    }
}