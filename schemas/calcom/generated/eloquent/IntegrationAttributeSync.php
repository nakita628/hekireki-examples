<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class IntegrationAttributeSync extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'IntegrationAttributeSync';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organizationId',
        'name',
        'integration',
        'credentialId',
        'enabled',
    ];

    protected $casts = [
        'organizationId' => 'integer',
        'credentialId' => 'integer',
        'enabled' => 'boolean',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function credential(): BelongsTo
    {
        return $this->belongsTo(Credential::class, 'credentialId');
    }

    public function attributeSyncRule(): HasOne
    {
        return $this->hasOne(AttributeSyncRule::class, 'integrationAttributeSyncId');
    }

    public function syncFieldMappings(): HasMany
    {
        return $this->hasMany(AttributeSyncFieldMapping::class, 'integrationAttributeSyncId');
    }
}