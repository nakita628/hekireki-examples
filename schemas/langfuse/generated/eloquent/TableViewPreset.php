<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TableViewPreset extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'table_view_presets';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'table_name',
        'created_by',
        'updated_by',
        'filters',
        'column_order',
        'column_visibility',
        'search_query',
        'order_by',
    ];

    protected $casts = [
        'filters' => 'array',
        'column_order' => 'array',
        'column_visibility' => 'array',
        'order_by' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function updatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updated_by');
    }
}