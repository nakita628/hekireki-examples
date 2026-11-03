<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Credits extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Credits';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organizationId',
        'credits',
        'type',
    ];

    protected $attributes = [
        'type' => 'ai_images',
    ];

    protected $casts = [
        'credits' => 'integer',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}