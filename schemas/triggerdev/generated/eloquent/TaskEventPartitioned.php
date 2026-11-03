<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TaskEventPartitioned extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const KEY_COLUMNS = ['id', 'createdAt'];

    protected $table = 'TaskEventPartitioned';

    protected $primaryKey = null;

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

    public function getKey()
    {
        $key = [];
        foreach (static::KEY_COLUMNS as $column) {
            $key[$column] = $this->getAttribute($column);
        }

        return $key;
    }

    public function newEloquentBuilder($query)
    {
        return new CompositeKeyBuilder($query);
    }

    public function newCollection(array $models = [])
    {
        return new CompositeKeyCollection($models);
    }

    public static function destroy($ids)
    {
        $count = 0;
        foreach ((new static())->newQuery()->whereKey(func_num_args() > 1 ? func_get_args() : $ids)->get() as $model) {
            if ($model->delete()) {
                $count++;
            }
        }

        return $count;
    }

    protected function setKeysForSaveQuery($query)
    {
        foreach (static::KEY_COLUMNS as $column) {
            $query->where($column, '=', $this->original[$column] ?? $this->getAttribute($column));
        }

        return $query;
    }

    protected function setKeysForSelectQuery($query)
    {
        foreach (static::KEY_COLUMNS as $column) {
            $query->where($column, '=', $this->original[$column] ?? $this->getAttribute($column));
        }

        return $query;
    }

    public function delete()
    {
        $this->mergeAttributesFromCachedCasts();

        if (! $this->exists) {
            return null;
        }

        if ($this->fireModelEvent('deleting') === false) {
            return false;
        }

        $this->touchOwners();
        $this->performDeleteOnModel();
        $this->fireModelEvent('deleted', false);

        return true;
    }
}