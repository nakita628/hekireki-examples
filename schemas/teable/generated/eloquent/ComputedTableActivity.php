<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ComputedTableActivity extends Model
{
    use PrismaDates;

    const CREATED_AT = null;

    protected $table = 'computed_table_activity';

    protected $primaryKey = 'table_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'table_id',
        'base_id',
        'status',
        'calculating_field_count',
        'queued_field_count',
        'estimated_complexity',
        'recent_completions',
        'generation',
    ];

    protected $attributes = [
        'calculating_field_count' => 0,
        'queued_field_count' => 0,
        'estimated_complexity' => 0,
        'recent_completions' => '[]',
        'generation' => 0,
    ];

    protected $casts = [
        'calculating_field_count' => 'integer',
        'queued_field_count' => 'integer',
        'estimated_complexity' => 'integer',
        'recent_completions' => 'array',
        'generation' => 'integer',
    ];
}