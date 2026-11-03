<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Notification extends Model
{
    use PrismaDates;

    protected $table = 'notification';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'from_user_id',
        'to_user_id',
        'type',
        'message',
        'message_i18n',
        'severity',
        'url_path',
        'is_read',
        'created_time',
        'created_by',
    ];

    protected $attributes = [
        'severity' => 'info',
        'is_read' => false,
    ];

    protected $casts = [
        'is_read' => 'boolean',
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}