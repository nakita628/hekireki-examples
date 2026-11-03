<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RecordRemovalTombstone extends Model
{
    use PrismaDates;

    protected $table = 'record_removal_tombstone';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'table_id',
        'record_id',
        'type',
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