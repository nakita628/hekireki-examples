<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TwoFactorBackupCode extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at'];

    protected $table = 'two_factor_backup_code';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'code_hash',
        'used',
    ];

    protected $attributes = [
        'used' => false,
    ];

    protected $casts = [
        'used' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}