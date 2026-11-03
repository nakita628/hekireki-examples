<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DashboardWidget extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'dashboard_widgets';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'created_by',
        'updated_by',
        'project_id',
        'name',
        'description',
        'view',
        'dimensions',
        'metrics',
        'filters',
        'chart_type',
        'chart_config',
        'min_version',
    ];

    protected $attributes = [
        'min_version' => 1,
    ];

    protected $casts = [
        'view' => DashboardWidgetViews::class,
        'dimensions' => 'array',
        'metrics' => 'array',
        'filters' => 'array',
        'chart_type' => DashboardWidgetChartType::class,
        'chart_config' => 'array',
        'min_version' => 'integer',
    ];

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function updatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updated_by');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}