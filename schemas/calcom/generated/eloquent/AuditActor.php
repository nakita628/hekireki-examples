<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasUuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class AuditActor extends Model
{
    use HasUuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AuditActor';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'type',
        'userUuid',
        'attendeeId',
        'credentialId',
        'email',
        'phone',
        'name',
    ];

    protected $casts = [
        'type' => AuditActorType::class,
        'attendeeId' => 'integer',
        'credentialId' => 'integer',
    ];

    public function bookingAudits(): HasMany
    {
        return $this->hasMany(BookingAudit::class, 'actorId');
    }
}