<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CustomField extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CustomField';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'identifier',
        'label',
        'placeholder',
        'required',
        'disabled',
        'linkId',
        'orderIndex',
    ];

    protected $attributes = [
        'required' => false,
        'disabled' => false,
        'orderIndex' => 0,
    ];

    protected $casts = [
        'type' => CustomFieldType::class,
        'required' => 'boolean',
        'disabled' => 'boolean',
        'orderIndex' => 'integer',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }
}