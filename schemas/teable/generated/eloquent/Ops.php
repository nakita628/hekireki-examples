<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Ops extends Model
{
    use PrismaDates;

    protected $table = 'ops';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'collection',
        'doc_id',
        'doc_type',
        'version',
        'operation',
        'created_time',
        'created_by',
    ];

    protected $casts = [
        'version' => 'integer',
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