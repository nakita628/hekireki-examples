<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class RuntimeEnvironmentSession extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RuntimeEnvironmentSession';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'ipAddress',
        'environmentId',
        'disconnectedAt',
    ];

    protected $casts = [
        'disconnectedAt' => 'datetime',
    ];

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function currentEnvironments(): HasMany
    {
        return $this->hasMany(RuntimeEnvironment::class, 'currentSessionId');
    }
}