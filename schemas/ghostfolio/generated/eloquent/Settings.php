<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Settings extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Settings';

    protected $primaryKey = 'userId';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'settings',
    ];

    protected $casts = [
        'settings' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}