<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class InstalledIntegration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'InstalledIntegration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'credentials',
        'configuration',
        'enabled',
        'integrationId',
        'userId',
        'teamId',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'credentials' => 'array',
        'configuration' => 'array',
        'enabled' => 'boolean',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}