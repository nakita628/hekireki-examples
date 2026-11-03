<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ViewerGroupAccessControls extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ViewerGroupAccessControls';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'groupId',
        'itemId',
        'itemType',
        'canView',
        'canDownload',
    ];

    protected $attributes = [
        'canView' => true,
        'canDownload' => false,
    ];

    protected $casts = [
        'itemType' => ItemType::class,
        'canView' => 'boolean',
        'canDownload' => 'boolean',
    ];

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }
}