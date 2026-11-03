<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Comment extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'comment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'table_id',
        'record_id',
        'quote_Id',
        'content',
        'reaction',
        'deleted_time',
        'created_time',
        'created_by',
    ];

    protected $casts = [
        'deleted_time' => 'datetime',
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