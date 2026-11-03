<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class HostGroup extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'HostGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'eventTypeId',
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
    ];

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function hosts(): HasMany
    {
        return $this->hasMany(Host::class, 'groupId');
    }
}