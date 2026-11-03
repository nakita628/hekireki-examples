<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Webhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Webhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'url',
        'method',
        'queryParams',
        'headers',
        'body',
        'typebotId',
    ];

    protected $casts = [
        'queryParams' => 'array',
        'headers' => 'array',
    ];

    public function typebot(): BelongsTo
    {
        return $this->belongsTo(Typebot::class, 'typebotId');
    }
}