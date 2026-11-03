<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class MockServerActivity extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['performedAt'];

    protected $table = 'MockServerActivity';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'mockServerID',
        'action',
        'performedBy',
        'performedAt',
    ];

    protected $casts = [
        'action' => MockServerAction::class,
        'performedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('performedAt', $this->attributes)) {
                $this->setAttribute('performedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function mockServer(): BelongsTo
    {
        return $this->belongsTo(MockServer::class, 'mockServerID');
    }
}