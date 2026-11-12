<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AiScheduledTask extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['lastRun', 'createdAt', 'updatedAt'];

    protected $table = 'aiScheduledTask';

    protected $fillable = [
        'name',
        'prompt',
        'schedule',
        'isEnabled',
        'lastRun',
        'lastResult',
        'accountId',
    ];

    protected $attributes = [
        'isEnabled' => true,
    ];

    protected $casts = [
        'isEnabled' => 'boolean',
        'lastRun' => 'datetime',
        'lastResult' => 'array',
        'accountId' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }
}