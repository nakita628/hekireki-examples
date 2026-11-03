<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SupportAccessRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SupportAccessRequest';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organizationId',
        'requestedById',
        'reason',
        'status',
        'approvedById',
        'approvedAt',
        'expiresAt',
        'cancelledById',
        'cancelledAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => SupportAccessRequestStatus::class,
        'approvedAt' => 'datetime',
        'expiresAt' => 'datetime',
        'cancelledAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function requestedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'requestedById');
    }

    public function approvedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'approvedById');
    }
}