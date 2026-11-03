<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraKnowledgeRecords extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['capturedAtZ', 'whenZ', 'deletedAtZ'];

    protected $table = 'mastra_knowledge_records';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'node',
        'text',
        'scope',
        'scopeKey',
        'sourceThreadId',
        'capturedAt',
        'when',
        'maxScope',
        'metadata',
        'deletedAt',
        'deletedBy',
        'capturedAtZ',
        'whenZ',
        'deletedAtZ',
    ];

    protected $casts = [
        'scope' => 'array',
        'capturedAt' => 'datetime',
        'when' => 'datetime',
        'metadata' => 'array',
        'deletedAt' => 'datetime',
        'capturedAtZ' => 'datetime',
        'whenZ' => 'datetime',
        'deletedAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('capturedAtZ', $this->attributes)) {
                $this->setAttribute('capturedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('whenZ', $this->attributes)) {
                $this->setAttribute('whenZ', $this->freshTimestamp());
            }
            if (! array_key_exists('deletedAtZ', $this->attributes)) {
                $this->setAttribute('deletedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}