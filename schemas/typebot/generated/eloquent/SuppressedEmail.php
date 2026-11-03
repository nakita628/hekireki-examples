<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SuppressedEmail extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SuppressedEmail';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'transientGeneralBounceCount',
        'suppressedAt',
        'lastWebhookId',
    ];

    protected $attributes = [
        'transientGeneralBounceCount' => 0,
    ];

    protected $casts = [
        'transientGeneralBounceCount' => 'integer',
        'suppressedAt' => 'datetime',
    ];
}