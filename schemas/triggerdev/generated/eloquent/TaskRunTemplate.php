<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TaskRunTemplate extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunTemplate';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'taskSlug',
        'triggerSource',
        'label',
        'payload',
        'payloadType',
        'metadata',
        'metadataType',
        'queue',
        'concurrencyKey',
        'ttlSeconds',
        'maxAttempts',
        'maxDurationSeconds',
        'tags',
        'machinePreset',
        'projectId',
        'organizationId',
    ];

    protected $attributes = [
        'payloadType' => 'application/json',
        'metadataType' => 'application/json',
    ];

    protected $casts = [
        'triggerSource' => TaskTriggerSource::class,
        'ttlSeconds' => 'integer',
        'maxAttempts' => 'integer',
        'maxDurationSeconds' => 'integer',
    ];
}