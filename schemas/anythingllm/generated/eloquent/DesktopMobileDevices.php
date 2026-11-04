<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DesktopMobileDevices extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'desktop_mobile_devices';

    protected $fillable = [
        'deviceOs',
        'deviceName',
        'token',
        'approved',
        'userId',
    ];

    protected $attributes = [
        'approved' => false,
    ];

    protected $casts = [
        'approved' => 'boolean',
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }
}