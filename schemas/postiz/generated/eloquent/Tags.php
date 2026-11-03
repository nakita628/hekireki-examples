<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Tags extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Tags';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'color',
        'orgId',
        'deletedAt',
    ];

    protected $casts = [
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'orgId');
    }

    public function posts(): HasMany
    {
        return $this->hasMany(TagsPosts::class, 'tagId');
    }
}