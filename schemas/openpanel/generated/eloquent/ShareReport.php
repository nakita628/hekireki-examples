<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ShareReport extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'share_reports';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'reportId',
        'organizationId',
        'projectId',
        'public',
        'password',
    ];

    protected $attributes = [
        'public' => false,
    ];

    protected $casts = [
        'public' => 'boolean',
    ];

    public function report(): BelongsTo
    {
        return $this->belongsTo(Report::class, 'reportId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}