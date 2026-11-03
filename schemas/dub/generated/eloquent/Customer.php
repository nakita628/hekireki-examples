<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Customer extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Customer';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'normalizedEmail',
        'avatar',
        'externalId',
        'stripeCustomerId',
        'linkId',
        'clickId',
        'clickedAt',
        'country',
        'sales',
        'saleAmount',
        'firstSaleAt',
        'subscriptionCanceledAt',
        'projectId',
        'projectConnectId',
    ];

    protected $attributes = [
        'sales' => 0,
        'saleAmount' => 0,
    ];

    protected $casts = [
        'clickedAt' => 'datetime',
        'sales' => 'integer',
        'saleAmount' => 'integer',
        'firstSaleAt' => 'datetime',
        'subscriptionCanceledAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }
}