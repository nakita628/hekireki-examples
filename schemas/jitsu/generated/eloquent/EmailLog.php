<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class EmailLog extends Model
{
    use PrismaDates;

    protected $table = 'EmailLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'time',
        'email',
        'error',
        'previewUrl',
        'messageId',
        'status',
    ];

    protected $casts = [
        'time' => 'datetime',
        'email' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('time', $this->attributes)) {
                $this->setAttribute('time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}