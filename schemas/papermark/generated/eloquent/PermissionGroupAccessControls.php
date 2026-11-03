<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PermissionGroupAccessControls extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PermissionGroupAccessControls';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'groupId',
        'itemId',
        'itemType',
        'canView',
        'canDownload',
        'canDownloadOriginal',
    ];

    protected $attributes = [
        'canView' => true,
        'canDownload' => false,
        'canDownloadOriginal' => false,
    ];

    protected $casts = [
        'itemType' => ItemType::class,
        'canView' => 'boolean',
        'canDownload' => 'boolean',
        'canDownloadOriginal' => 'boolean',
    ];

    public function group(): BelongsTo
    {
        return $this->belongsTo(PermissionGroup::class, 'groupId');
    }
}