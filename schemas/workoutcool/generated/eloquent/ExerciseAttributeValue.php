<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ExerciseAttributeValue extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'exercise_attribute_values';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'attributeNameId',
        'value',
    ];

    protected $casts = [
        'value' => ExerciseAttributeValueEnum::class,
    ];

    public function attributeName(): BelongsTo
    {
        return $this->belongsTo(ExerciseAttributeName::class, 'attributeNameId');
    }

    public function attributes(): HasMany
    {
        return $this->hasMany(ExerciseAttribute::class, 'attributeValueId');
    }
}