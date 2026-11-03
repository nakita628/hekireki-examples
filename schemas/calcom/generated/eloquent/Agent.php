<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Agent extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Agent';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'userId',
        'teamId',
        'providerAgentId',
        'inboundEventTypeId',
        'outboundEventTypeId',
        'enabled',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
        'inboundEventTypeId' => 'integer',
        'outboundEventTypeId' => 'integer',
        'enabled' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function inboundPhoneNumbers(): HasMany
    {
        return $this->hasMany(CalAiPhoneNumber::class, 'inboundAgentId');
    }

    public function outboundPhoneNumbers(): HasMany
    {
        return $this->hasMany(CalAiPhoneNumber::class, 'outboundAgentId');
    }
}