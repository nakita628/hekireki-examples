<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ScoreConfig extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'score_configs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'data_type',
        'is_archived',
        'min_value',
        'max_value',
        'categories',
        'description',
    ];

    protected $attributes = [
        'is_archived' => false,
    ];

    protected $casts = [
        'data_type' => ScoreConfigDataType::class,
        'is_archived' => 'boolean',
        'min_value' => 'float',
        'max_value' => 'float',
        'categories' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}