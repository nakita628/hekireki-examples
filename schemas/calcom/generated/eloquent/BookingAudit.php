<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasUuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BookingAudit extends Model
{
    use HasUuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BookingAudit';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'bookingUid',
        'actorId',
        'type',
        'action',
        'timestamp',
        'source',
        'operationId',
        'data',
        'context',
    ];

    protected $casts = [
        'type' => BookingAuditType::class,
        'action' => BookingAuditAction::class,
        'timestamp' => 'datetime',
        'source' => BookingAuditSource::class,
        'data' => 'array',
        'context' => 'array',
    ];

    public function actor(): BelongsTo
    {
        return $this->belongsTo(AuditActor::class, 'actorId');
    }
}