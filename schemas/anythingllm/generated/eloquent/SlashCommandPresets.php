<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SlashCommandPresets extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'slash_command_presets';

    protected $fillable = [
        'command',
        'prompt',
        'description',
        'uid',
        'userId',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'uid' => 0,
    ];

    protected $casts = [
        'uid' => 'integer',
        'userId' => 'integer',
        'lastUpdatedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }
}