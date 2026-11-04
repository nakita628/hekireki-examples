<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SystemPromptVariables extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'system_prompt_variables';

    protected $fillable = [
        'key',
        'value',
        'description',
        'type',
        'userId',
    ];

    protected $attributes = [
        'type' => 'system',
    ];

    protected $casts = [
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }
}