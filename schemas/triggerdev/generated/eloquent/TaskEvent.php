<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TaskEvent extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'TaskEvent';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'message',
        'traceId',
        'spanId',
        'parentId',
        'tracestate',
        'isError',
        'isPartial',
        'isCancelled',
        'isDebug',
        'serviceName',
        'serviceNamespace',
        'level',
        'kind',
        'status',
        'links',
        'events',
        'startTime',
        'duration',
        'attemptId',
        'attemptNumber',
        'environmentId',
        'environmentType',
        'organizationId',
        'projectId',
        'projectRef',
        'runId',
        'runIsTest',
        'idempotencyKey',
        'taskSlug',
        'taskPath',
        'taskExportName',
        'workerId',
        'workerVersion',
        'queueId',
        'queueName',
        'batchId',
        'properties',
        'metadata',
        'style',
        'output',
        'outputType',
        'payload',
        'payloadType',
        'usageDurationMs',
        'usageCostInCents',
        'machinePreset',
        'machinePresetCpu',
        'machinePresetMemory',
        'machinePresetCentsPerMs',
    ];

    protected $attributes = [
        'isError' => false,
        'isPartial' => false,
        'isCancelled' => false,
        'isDebug' => false,
        'level' => 'TRACE',
        'kind' => 'INTERNAL',
        'status' => 'UNSET',
        'duration' => 0,
        'runIsTest' => false,
        'usageDurationMs' => 0,
        'usageCostInCents' => 0.0,
    ];

    protected $casts = [
        'isError' => 'boolean',
        'isPartial' => 'boolean',
        'isCancelled' => 'boolean',
        'isDebug' => 'boolean',
        'level' => TaskEventLevel::class,
        'kind' => TaskEventKind::class,
        'status' => TaskEventStatus::class,
        'links' => 'array',
        'events' => 'array',
        'startTime' => 'integer',
        'duration' => 'integer',
        'attemptNumber' => 'integer',
        'environmentType' => RuntimeEnvironmentType::class,
        'runIsTest' => 'boolean',
        'properties' => 'array',
        'metadata' => 'array',
        'style' => 'array',
        'output' => 'array',
        'payload' => 'array',
        'usageDurationMs' => 'integer',
        'usageCostInCents' => 'float',
        'machinePresetCpu' => 'float',
        'machinePresetMemory' => 'float',
        'machinePresetCentsPerMs' => 'float',
    ];
}