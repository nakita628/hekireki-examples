<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CustomerQuery extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'CustomerQuery';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'query',
        'scope',
        'stats',
        'title',
        'source',
        'organizationId',
        'projectId',
        'environmentId',
        'userId',
        'filterPeriod',
        'filterFrom',
        'filterTo',
    ];

    protected $attributes = [
        'source' => 'DASHBOARD',
    ];

    protected $casts = [
        'scope' => CustomerQueryScope::class,
        'stats' => 'array',
        'source' => CustomerQuerySource::class,
        'filterFrom' => 'datetime',
        'filterTo' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}