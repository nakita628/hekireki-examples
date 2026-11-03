<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Plugs extends Model
{
    use HasVersion4Uuids;

    protected $table = 'Plugs';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'organizationId',
        'plugFunction',
        'data',
        'integrationId',
        'activated',
    ];

    protected $attributes = [
        'activated' => true,
    ];

    protected $casts = [
        'activated' => 'boolean',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}