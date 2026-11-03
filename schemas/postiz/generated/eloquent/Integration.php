<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Integration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Integration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'internalId',
        'organizationId',
        'name',
        'customName',
        'picture',
        'providerIdentifier',
        'type',
        'token',
        'disabled',
        'tokenExpiration',
        'refreshToken',
        'profile',
        'deletedAt',
        'inBetweenSteps',
        'refreshNeeded',
        'postingTimes',
        'customInstanceDetails',
        'customerId',
        'rootInternalId',
        'additionalSettings',
    ];

    protected $attributes = [
        'disabled' => false,
        'inBetweenSteps' => false,
        'refreshNeeded' => false,
        'postingTimes' => '[{"time":120}, {"time":400}, {"time":700}]',
        'additionalSettings' => '[]',
    ];

    protected $casts = [
        'disabled' => 'boolean',
        'tokenExpiration' => 'datetime',
        'deletedAt' => 'datetime',
        'inBetweenSteps' => 'boolean',
        'refreshNeeded' => 'boolean',
    ];

    public function customer(): BelongsTo
    {
        return $this->belongsTo(Customer::class, 'customerId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function exisingPlugData(): HasMany
    {
        return $this->hasMany(ExisingPlugData::class, 'integrationId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(IntegrationsWebhooks::class, 'integrationId');
    }

    public function orderItems(): HasMany
    {
        return $this->hasMany(OrderItems::class, 'integrationId');
    }

    public function plugs(): HasMany
    {
        return $this->hasMany(Plugs::class, 'integrationId');
    }

    public function posts(): HasMany
    {
        return $this->hasMany(Post::class, 'integrationId');
    }
}