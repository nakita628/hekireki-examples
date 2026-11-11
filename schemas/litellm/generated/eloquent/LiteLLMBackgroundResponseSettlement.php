<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMBackgroundResponseSettlement extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_BackgroundResponseSettlement';

    protected $primaryKey = 'response_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'response_id',
        'settled_at',
    ];

    protected $casts = [
        'settled_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('settled_at', $this->attributes)) {
                $this->setAttribute('settled_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}