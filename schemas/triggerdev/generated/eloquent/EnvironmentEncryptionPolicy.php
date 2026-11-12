<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EnvironmentEncryptionPolicy extends Model
{
    protected $table = 'EnvironmentEncryptionPolicy';

    protected $primaryKey = 'environmentId';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'environmentId',
        'revision',
        'wrappingProvider',
        'wrappingKeyReference',
        'activeKeyId',
    ];

    protected $attributes = [
        'revision' => 0,
    ];

    protected $casts = [
        'revision' => 'integer',
        'wrappingProvider' => EnvironmentKeyWrappingProvider::class,
    ];

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}