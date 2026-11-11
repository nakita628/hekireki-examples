<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Alert extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['expiresAt', 'observedConditionChangedAt', 'pendingMetSince', 'evaluationLeaseUntil', 'lastEvaluatedAt', 'nextEvaluationAt', 'createdAt', 'updatedAt', 'resolvedAt', 'stoppedAt', 'lastOccurrenceAt'];

    protected $table = 'Alert';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organizationId',
        'projectId',
        'environmentId',
        'capabilityKey',
        'params',
        'condition',
        'query',
        'expiresAt',
        'lifecycle',
        'attention',
        'observedCondition',
        'observedConditionChangedAt',
        'pendingMetSince',
        'evaluatorState',
        'evaluationLeaseId',
        'evaluationLeaseUntil',
        'lastEvaluatedAt',
        'nextEvaluationAt',
        'notifiers',
        'name',
        'createdBy',
        'resolvedAt',
        'resolvedBy',
        'stoppedAt',
        'stoppedBy',
        'lastOccurrenceAt',
        'occurrencesSinceRaise',
        'occurrencesTotal',
        'groupId',
    ];

    protected $attributes = [
        'lifecycle' => 'active',
        'attention' => 'clear',
        'observedCondition' => 'unknown',
        'notifiers' => '[]',
        'occurrencesSinceRaise' => 0,
        'occurrencesTotal' => 0,
    ];

    protected $casts = [
        'params' => 'array',
        'condition' => 'array',
        'expiresAt' => 'datetime',
        'observedConditionChangedAt' => 'datetime',
        'pendingMetSince' => 'datetime',
        'evaluatorState' => 'array',
        'evaluationLeaseUntil' => 'datetime',
        'lastEvaluatedAt' => 'datetime',
        'nextEvaluationAt' => 'datetime',
        'notifiers' => 'array',
        'resolvedAt' => 'datetime',
        'stoppedAt' => 'datetime',
        'lastOccurrenceAt' => 'datetime',
        'occurrencesSinceRaise' => 'integer',
        'occurrencesTotal' => 'integer',
    ];
}