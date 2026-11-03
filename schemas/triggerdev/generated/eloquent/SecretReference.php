<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SecretReference extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SecretReference';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'key',
        'provider',
    ];

    protected $attributes = [
        'provider' => 'DATABASE',
    ];

    protected $casts = [
        'provider' => SecretStoreProvider::class,
    ];

    public function environmentVariableValues(): HasMany
    {
        return $this->hasMany(EnvironmentVariableValue::class, 'valueReferenceId');
    }

    public function OrganizationIntegration(): HasMany
    {
        return $this->hasMany(OrganizationIntegration::class, 'tokenReferenceId');
    }

    public function User(): HasMany
    {
        return $this->hasMany(User::class, 'mfaSecretReferenceId');
    }
}