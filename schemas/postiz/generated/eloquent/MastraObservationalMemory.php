<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraObservationalMemory extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['lastObservedAtZ', 'lastReflectionAtZ', 'lastBufferedAtTimeZ', 'createdAtZ', 'updatedAtZ'];

    protected $table = 'mastra_observational_memory';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'lookupKey',
        'scope',
        'resourceId',
        'threadId',
        'activeObservations',
        'activeObservationsPendingUpdate',
        'originType',
        'config',
        'generationCount',
        'lastObservedAt',
        'lastReflectionAt',
        'pendingMessageTokens',
        'totalTokensObserved',
        'observationTokenCount',
        'isObserving',
        'isReflecting',
        'observedMessageIds',
        'observedTimezone',
        'bufferedObservations',
        'bufferedObservationTokens',
        'bufferedMessageIds',
        'bufferedReflection',
        'bufferedReflectionTokens',
        'bufferedReflectionInputTokens',
        'reflectedObservationLineCount',
        'bufferedObservationChunks',
        'isBufferingObservation',
        'isBufferingReflection',
        'lastBufferedAtTokens',
        'lastBufferedAtTime',
        'metadata',
        'lastObservedAtZ',
        'lastReflectionAtZ',
        'lastBufferedAtTimeZ',
        'createdAtZ',
        'updatedAtZ',
    ];

    protected $casts = [
        'generationCount' => 'integer',
        'lastObservedAt' => 'datetime',
        'lastReflectionAt' => 'datetime',
        'pendingMessageTokens' => 'integer',
        'totalTokensObserved' => 'integer',
        'observationTokenCount' => 'integer',
        'isObserving' => 'boolean',
        'isReflecting' => 'boolean',
        'observedMessageIds' => 'array',
        'bufferedObservationTokens' => 'integer',
        'bufferedMessageIds' => 'array',
        'bufferedReflectionTokens' => 'integer',
        'bufferedReflectionInputTokens' => 'integer',
        'reflectedObservationLineCount' => 'integer',
        'bufferedObservationChunks' => 'array',
        'isBufferingObservation' => 'boolean',
        'isBufferingReflection' => 'boolean',
        'lastBufferedAtTokens' => 'integer',
        'lastBufferedAtTime' => 'datetime',
        'metadata' => 'array',
        'lastObservedAtZ' => 'datetime',
        'lastReflectionAtZ' => 'datetime',
        'lastBufferedAtTimeZ' => 'datetime',
        'createdAtZ' => 'datetime',
        'updatedAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastObservedAtZ', $this->attributes)) {
                $this->setAttribute('lastObservedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('lastReflectionAtZ', $this->attributes)) {
                $this->setAttribute('lastReflectionAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('lastBufferedAtTimeZ', $this->attributes)) {
                $this->setAttribute('lastBufferedAtTimeZ', $this->freshTimestamp());
            }
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('updatedAtZ', $this->attributes)) {
                $this->setAttribute('updatedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}