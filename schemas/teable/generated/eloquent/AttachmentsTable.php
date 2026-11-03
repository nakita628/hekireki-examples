<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AttachmentsTable extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'attachments_table';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'attachment_id',
        'name',
        'token',
        'table_id',
        'record_id',
        'field_id',
        'created_time',
        'created_by',
        'last_modified_by',
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