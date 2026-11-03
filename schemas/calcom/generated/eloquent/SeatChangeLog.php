<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SeatChangeLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'SeatChangeLog';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'teamId',
        'changeType',
        'seatCount',
        'userId',
        'triggeredBy',
        'changeDate',
        'monthKey',
        'operationId',
        'processedInProrationId',
        'metadata',
        'teamBillingId',
        'organizationBillingId',
    ];

    protected $casts = [
        'teamId' => 'integer',
        'changeType' => SeatChangeType::class,
        'seatCount' => 'integer',
        'userId' => 'integer',
        'triggeredBy' => 'integer',
        'changeDate' => 'datetime',
        'metadata' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('changeDate', $this->attributes)) {
                $this->setAttribute('changeDate', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function proration(): BelongsTo
    {
        return $this->belongsTo(MonthlyProration::class, 'processedInProrationId');
    }

    public function teamBilling(): BelongsTo
    {
        return $this->belongsTo(TeamBilling::class, 'teamBillingId');
    }

    public function organizationBilling(): BelongsTo
    {
        return $this->belongsTo(OrganizationBilling::class, 'organizationBillingId');
    }
}