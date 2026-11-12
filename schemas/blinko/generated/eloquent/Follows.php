<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Follows extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'follows';

    protected $fillable = [
        'siteName',
        'siteUrl',
        'siteAvatar',
        'description',
        'followType',
        'accountId',
    ];

    protected $attributes = [
        'followType' => 'following',
    ];

    protected $casts = [
        'accountId' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }
}