<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TrustedDeployRepository extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TrustedDeployRepository';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'issuer',
        'repositoryOwner',
        'repositoryName',
        'repositoryId',
        'repositoryOwnerId',
    ];

    protected $attributes = [
        'issuer' => 'https://token.actions.githubusercontent.com',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environments(): HasMany
    {
        return $this->hasMany(TrustedDeployEnvironment::class, 'trustedDeployRepositoryId');
    }
}