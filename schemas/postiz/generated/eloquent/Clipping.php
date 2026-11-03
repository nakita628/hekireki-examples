<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Clipping extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Clipping';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organizationId',
        'url',
        'status',
        'error',
        'title',
        'thumbnail',
        'duration',
        'maxClips',
        'fit',
        'integrations',
        'creditsId',
        'deletedAt',
    ];

    protected $attributes = [
        'status' => 'analysing',
        'maxClips' => 5,
        'fit' => 'blur',
    ];

    protected $casts = [
        'duration' => 'integer',
        'maxClips' => 'integer',
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function clips(): HasMany
    {
        return $this->hasMany(ClippingClip::class, 'clippingId');
    }
}