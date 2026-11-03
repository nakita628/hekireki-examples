<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EnvironmentVariableValue extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EnvironmentVariableValue';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'valueReferenceId',
        'variableId',
        'environmentId',
        'isSecret',
        'version',
        'lastUpdatedBy',
    ];

    protected $attributes = [
        'isSecret' => false,
        'version' => 1,
    ];

    protected $casts = [
        'isSecret' => 'boolean',
        'version' => 'integer',
        'lastUpdatedBy' => 'array',
    ];

    public function valueReference(): BelongsTo
    {
        return $this->belongsTo(SecretReference::class, 'valueReferenceId');
    }

    public function variable(): BelongsTo
    {
        return $this->belongsTo(EnvironmentVariable::class, 'variableId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}