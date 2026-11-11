<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMRetiredAgent extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_RetiredAgent';

    protected $primaryKey = 'original_agent_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'original_agent_id',
        'retired_at',
    ];

    protected $casts = [
        'retired_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('retired_at', $this->attributes)) {
                $this->setAttribute('retired_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}