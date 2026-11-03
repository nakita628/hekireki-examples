<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ComputedUpdatePauseScope extends Model
{
    use PrismaDates;

    const CREATED_AT = null;

    protected $table = 'computed_update_pause_scope';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'scope_type',
        'scope_id',
        'paused_at',
        'paused_by',
        'resume_at',
        'reason',
        'write_policy',
        'updated_by',
    ];

    protected $attributes = [
        'write_policy' => 'allow_bounded',
    ];

    protected $casts = [
        'paused_at' => 'datetime',
        'resume_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('paused_at', $this->attributes)) {
                $this->setAttribute('paused_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}