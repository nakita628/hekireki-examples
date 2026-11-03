<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RegisteredDomain extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RegisteredDomain';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'projectId',
        'domainId',
        'autoRenewalDisabledAt',
        'renewalFee',
        'expiresAt',
    ];

    protected $attributes = [
        'renewalFee' => 1200,
    ];

    protected $casts = [
        'autoRenewalDisabledAt' => 'datetime',
        'renewalFee' => 'integer',
        'expiresAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function domain(): BelongsTo
    {
        return $this->belongsTo(Domain::class, 'domainId');
    }
}