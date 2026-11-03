<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AutoPost extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AutoPost';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organizationId',
        'title',
        'content',
        'onSlot',
        'syncLast',
        'url',
        'lastUrl',
        'active',
        'addPicture',
        'generateContent',
        'integrations',
        'deletedAt',
    ];

    protected $casts = [
        'onSlot' => 'boolean',
        'syncLast' => 'boolean',
        'active' => 'boolean',
        'addPicture' => 'boolean',
        'generateContent' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}