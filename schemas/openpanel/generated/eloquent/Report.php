<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Report extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'reports';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'interval',
        'range',
        'chartType',
        'lineType',
        'breakdowns',
        'events',
        'globalFilters',
        'formula',
        'unit',
        'metric',
        'projectId',
        'previous',
        'criteria',
        'funnelGroup',
        'funnelWindow',
        'options',
        'display',
        'visibleSeries',
        'startDate',
        'endDate',
        'dashboardId',
    ];

    protected $attributes = [
        'range' => '30d',
        'lineType' => 'monotone',
        'globalFilters' => '[]',
        'metric' => 'sum',
        'previous' => false,
    ];

    protected $casts = [
        'interval' => Interval::class,
        'chartType' => ChartType::class,
        'breakdowns' => 'array',
        'events' => 'array',
        'globalFilters' => 'array',
        'metric' => Metric::class,
        'previous' => 'boolean',
        'funnelWindow' => 'float',
        'options' => 'array',
        'display' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function dashboard(): BelongsTo
    {
        return $this->belongsTo(Dashboard::class, 'dashboardId');
    }

    public function layout(): HasOne
    {
        return $this->hasOne(ReportLayout::class, 'reportId');
    }

    public function share(): HasOne
    {
        return $this->hasOne(ShareReport::class, 'reportId');
    }
}