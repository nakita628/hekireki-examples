<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ErrorGroupState extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ErrorGroupState';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organizationId',
        'projectId',
        'environmentId',
        'taskIdentifier',
        'errorFingerprint',
        'status',
        'ignoredUntil',
        'ignoredUntilOccurrenceRate',
        'ignoredUntilTotalOccurrences',
        'ignoredAtOccurrenceCount',
        'ignoredAt',
        'ignoredReason',
        'ignoredByUserId',
        'resolvedAt',
        'resolvedInVersion',
        'resolvedBy',
    ];

    protected $attributes = [
        'status' => 'UNRESOLVED',
    ];

    protected $casts = [
        'status' => ErrorGroupStatus::class,
        'ignoredUntil' => 'datetime',
        'ignoredUntilOccurrenceRate' => 'integer',
        'ignoredUntilTotalOccurrences' => 'integer',
        'ignoredAtOccurrenceCount' => 'integer',
        'ignoredAt' => 'datetime',
        'resolvedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}