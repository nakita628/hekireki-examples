<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Dashboard extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'dashboards';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'created_by',
        'updated_by',
        'project_id',
        'name',
        'description',
        'definition',
        'filters',
    ];

    protected $attributes = [
        'filters' => '[]',
    ];

    protected $casts = [
        'definition' => 'array',
        'filters' => 'array',
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

    public function homeDashboardProjects(): HasMany
    {
        return $this->hasMany(Project::class, 'home_dashboard_id');
    }
}