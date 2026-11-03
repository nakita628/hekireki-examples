<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SessionRun extends Model
{
    use PrismaDates;

    protected $table = 'SessionRun';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'sessionId',
        'runId',
        'reason',
        'triggeredAt',
    ];

    protected $casts = [
        'triggeredAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('triggeredAt', $this->attributes)) {
                $this->setAttribute('triggeredAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function session(): BelongsTo
    {
        return $this->belongsTo(Session::class, 'sessionId');
    }
}