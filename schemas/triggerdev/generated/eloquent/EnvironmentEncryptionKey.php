<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EnvironmentEncryptionKey extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'EnvironmentEncryptionKey';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'environmentId',
        'wrappedKey',
        'wrappingProvider',
        'wrappingKeyReference',
        'revision',
    ];

    protected $attributes = [
        'revision' => 0,
    ];

    protected $casts = [
        'wrappedKey' => 'array',
        'wrappingProvider' => EnvironmentKeyWrappingProvider::class,
        'revision' => 'integer',
    ];

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}