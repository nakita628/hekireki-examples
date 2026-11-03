<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EvaluationRuleEvaluatorAssignment extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'evaluation_rule_evaluator_assignments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'evaluation_rule_id',
        'evaluator_id',
        'variable_mapping',
    ];

    protected $casts = [
        'variable_mapping' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function evaluationRule(): BelongsTo
    {
        return $this->belongsTo(EvaluationRule::class, 'evaluation_rule_id');
    }

    public function evaluator(): BelongsTo
    {
        return $this->belongsTo(Evaluator::class, 'evaluator_id');
    }
}