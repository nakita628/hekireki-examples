<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Cohort extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'cohorts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'projectId',
        'definition',
        'isStatic',
        'profileCount',
        'lastComputedAt',
    ];

    protected $attributes = [
        'definition' => '{}',
        'isStatic' => false,
        'profileCount' => 0,
    ];

    protected $casts = [
        'definition' => 'array',
        'isStatic' => 'boolean',
        'profileCount' => 'integer',
        'lastComputedAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}