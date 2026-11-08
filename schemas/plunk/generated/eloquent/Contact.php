<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Contact extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'contacts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'email',
        'data',
        'subscribed',
        'snoozedUntil',
        'projectId',
    ];

    protected $attributes = [
        'subscribed' => true,
    ];

    protected $casts = [
        'data' => 'array',
        'subscribed' => 'boolean',
        'snoozedUntil' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(Email::class, 'contactId');
    }

    public function workflowExecutions(): HasMany
    {
        return $this->hasMany(WorkflowExecution::class, 'contactId');
    }

    public function events(): HasMany
    {
        return $this->hasMany(Event::class, 'contactId');
    }

    public function segmentMemberships(): HasMany
    {
        return $this->hasMany(SegmentMembership::class, 'contactId');
    }

    public function throttleClaims(): HasMany
    {
        return $this->hasMany(ThrottleClaim::class, 'contactId');
    }
}