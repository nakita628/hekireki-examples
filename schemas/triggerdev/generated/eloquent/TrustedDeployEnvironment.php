<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TrustedDeployEnvironment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TrustedDeployEnvironment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'trustedDeployRepositoryId',
        'runtimeEnvironmentId',
        'enabled',
        'scope',
        'scopeValue',
        'additionalClaims',
    ];

    protected $attributes = [
        'enabled' => false,
        'additionalClaims' => '{}',
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'scope' => TrustedDeployScope::class,
        'additionalClaims' => 'array',
    ];

    public function trustedDeployRepository(): BelongsTo
    {
        return $this->belongsTo(TrustedDeployRepository::class, 'trustedDeployRepositoryId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }
}