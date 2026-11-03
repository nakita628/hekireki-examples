<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class AttributeOption extends Model
{
    use HasVersion4Uuids;

    protected $table = 'AttributeOption';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'attributeId',
        'value',
        'slug',
        'isGroup',
        'contains',
    ];

    protected $attributes = [
        'isGroup' => false,
    ];

    protected $casts = [
        'isGroup' => 'boolean',
    ];

    public function attribute(): BelongsTo
    {
        return $this->belongsTo(Attribute::class, 'attributeId');
    }

    public function assignedUsers(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'attributeOptionId');
    }
}