<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CloudSpendAlert extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'cloud_spend_alerts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'org_id',
        'title',
        'threshold',
        'triggered_at',
    ];

    protected $casts = [
        'threshold' => AsDecimal::class,
        'triggered_at' => 'datetime',
    ];

    public function org(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }
}