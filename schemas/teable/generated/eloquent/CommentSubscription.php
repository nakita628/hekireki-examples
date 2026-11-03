<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class CommentSubscription extends Model
{
    use PrismaDates;

    protected $table = 'comment_subscription';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'table_id',
        'record_id',
        'created_by',
        'created_time',
    ];

    protected $casts = [
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