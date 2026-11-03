<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ProjectAlertStorage extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProjectAlertStorage';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'alertChannelId',
        'alertType',
        'storageId',
        'storageData',
    ];

    protected $casts = [
        'alertType' => ProjectAlertType::class,
        'storageData' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function alertChannel(): BelongsTo
    {
        return $this->belongsTo(ProjectAlertChannel::class, 'alertChannelId');
    }
}