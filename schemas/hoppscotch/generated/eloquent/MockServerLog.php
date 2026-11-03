<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class MockServerLog extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['executedAt'];

    protected $table = 'MockServerLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'mockServerID',
        'requestMethod',
        'requestPath',
        'requestHeaders',
        'requestBody',
        'requestQuery',
        'responseStatus',
        'responseHeaders',
        'responseBody',
        'responseTime',
        'ipAddress',
        'userAgent',
        'executedAt',
    ];

    protected $casts = [
        'requestHeaders' => 'array',
        'requestBody' => 'array',
        'requestQuery' => 'array',
        'responseStatus' => 'integer',
        'responseHeaders' => 'array',
        'responseBody' => 'array',
        'responseTime' => 'integer',
        'executedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('executedAt', $this->attributes)) {
                $this->setAttribute('executedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function mockServer(): BelongsTo
    {
        return $this->belongsTo(MockServer::class, 'mockServerID');
    }
}