<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EmailTransport extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EmailTransport';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'type',
        'fromName',
        'fromAddress',
        'config',
    ];

    protected $casts = [
        'type' => EmailTransportType::class,
    ];

    public function subscriptionClaims(): HasMany
    {
        return $this->hasMany(SubscriptionClaim::class, 'emailTransportId');
    }

    public function organisationClaims(): HasMany
    {
        return $this->hasMany(OrganisationClaim::class, 'emailTransportId');
    }
}